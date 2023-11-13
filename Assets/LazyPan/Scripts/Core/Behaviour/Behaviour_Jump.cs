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
                Debug.Log("jump");
                moveDir.y += Mathf.Sqrt(5 * -3.0f * 5);
            }
        }

        private void Update_Behaviour_Jump() {
            controller.Move(moveDir * Time.deltaTime);
        }
    }
}