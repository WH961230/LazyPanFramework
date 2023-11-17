using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Fly : Behaviour {
        private Vector2 inputFlyVec;
        private CharacterController controller;
        public Behaviour_Fly(int subjectID) : base(subjectID) {
            controller = SubjectComp.Get<CharacterController>("CharacterController");
            Input.Instance.Load("Player/Fly", Input_Fly);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Fly);
        }

        private void Input_Fly(InputAction.CallbackContext context) {
            inputFlyVec = context.ReadValue<Vector2>();
        }

        private void Update_Behaviour_Fly() {
            if (controller != null) {
                SubjectBehaviourData.MoveVec.y += (inputFlyVec.y - inputFlyVec.x) * Time.deltaTime * 20f;
                SubjectBehaviourData.IsFlying = inputFlyVec.y > 0 || inputFlyVec.x > 0;
            }
        }
    }
}