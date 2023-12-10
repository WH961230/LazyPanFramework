using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Fly : Behaviour {
        private Vector2 inputFlyVec;
        private CharacterController controller;

        public Behaviour_Fly(uint subjectID) : base(subjectID, "Behaviour_Fly") {
            controller = SubjectComp.Get<CharacterController>("CharacterController");
            Input.Instance.Load("Player/Fly", Input_Fly);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Fly);
        }

        private void Input_Fly(InputAction.CallbackContext context) {
            inputFlyVec = context.ReadValue<Vector2>();
        }

        private void Update_Behaviour_Fly() {
            if (!IsSelected()) {
                return;
            }

            if (controller != null) {
                SubjectBehaviourData.MoveVec.y += (inputFlyVec.y - inputFlyVec.x) * Time.deltaTime * 20f;
                SubjectBehaviourData.IsFlying = inputFlyVec.y > 0 || inputFlyVec.x > 0;
                if (SubjectBehaviourData.IsFlying) {
                    Sound.Instance.PlaySound("Fly", SubjectGo.UGo.transform.position, true);
                } else {
                    Sound.Instance.StopSound("Fly");
                }
            }
        }
    }
}