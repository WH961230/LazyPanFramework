using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Jump : Behaviour {
        private Vector3 moveDir;
        private CharacterController controller;

        public Behaviour_Jump(int subjectId) : base(subjectId) {
            Input.Instance.Load("Player/Jump", Input_Jump);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Jump);
            controller = SubjectGo.UGo.GetComponent<Comp>().Get<CharacterController>("CharacterController");
        }

        private void Input_Jump(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                moveDir.y += Mathf.Sqrt(50 * -3.0f * 10f);
            }
        }

        private void Update_Behaviour_Jump() {
            if (controller != null && moveDir != Vector3.zero) {
                controller.Move(moveDir * Time.deltaTime);
            }
        }
    }
}