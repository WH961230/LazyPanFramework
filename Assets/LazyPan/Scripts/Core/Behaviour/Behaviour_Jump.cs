using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Jump : Behaviour {
        private GameObject uGo;
        private Vector3 moveDir;
        private CharacterController controller;
        private float JumpHeight;

        public Behaviour_Jump(uint subjectId) : base(subjectId, "Behaviour_Jump") {
            Input.Instance.Load("Player/Jump", Input_Jump);
            uGo = SubjectGo.UGo;
            Comp goComp = uGo.GetComponent<Comp>();
            controller = goComp.Get<CharacterController>("CharacterController");
            SubjectBehaviourData.IsJumping = false;
            JumpHeight = 1;
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Jump);
        }

        private void Input_Jump(InputAction.CallbackContext callbackContext) {
            if (!IsSelected()) {
                return;
            }

            if (callbackContext.performed && SubjectBehaviourData.IsGrounded && !SubjectBehaviourData.IsJumping) {
                SubjectBehaviourData.MoveVec.y += Mathf.Sqrt(JumpHeight * -3.0f * SubjectBehaviourData.GravityValue);
                SubjectBehaviourData.IsJumping = true;
                Sound.Instance.PlaySound("Jump", SubjectUGo.transform.position, false);
            }
        }

        private void Update_Behaviour_Jump() {
            if (controller == null) {
                return;
            }

            if (controller != null) {
                if (!SubjectBehaviourData.IsGrounded) {
                    SubjectBehaviourData.IsJumping = false;
                }
            }
        }
    }
}