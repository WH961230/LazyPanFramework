using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Jump : Behaviour {
        private GameObject uGo;
        private Vector3 moveDir;
        private CharacterController controller;
        private bool isGrounded;
        private float GravityValue;
        private float JumpHeight;

        public Behaviour_Jump(int subjectId) : base(subjectId) {
            Input.Instance.Load("Player/Jump", Input_Jump);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Jump);
            uGo = SubjectGo.UGo;
            controller = uGo.GetComponent<Comp>().Get<CharacterController>("CharacterController");
            string[] parameter = BehaviourConfig.Get("Behaviour_Jump").Parameter.Split('|');
            JumpHeight = float.Parse(parameter[0]);
            GravityValue = float.Parse(parameter[1]);
        }

        private void Input_Jump(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                moveDir.y += Mathf.Sqrt(JumpHeight * -3.0f * GravityValue);
            }
        }

        private void Update_Behaviour_Jump() {
            Debug.Log("controller isGrounded :" + controller.isGrounded);
            if (controller != null && moveDir != Vector3.zero) {
                if (controller.isGrounded) {
                    moveDir.y = 0f;
                }

                moveDir.y += GravityValue * Time.deltaTime;
                controller.Move(moveDir * Time.deltaTime);
            }
        }
    }
}