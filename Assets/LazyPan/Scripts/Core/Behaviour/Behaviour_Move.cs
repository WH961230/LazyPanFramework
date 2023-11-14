using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Move : Behaviour {
        private Vector2 inputVec;
        private Vector3 moveDir;
        private CharacterController controller;
        public Behaviour_Move(int subjectId) : base(subjectId) {
            Input.Instance.Load("Player/Move", Input_Move);
            controller = SubjectGo.UGo.GetComponent<Comp>().Get<CharacterController>("CharacterController");
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Move);
        }

        private void Input_Move(InputAction.CallbackContext callbackContext) {
            inputVec = callbackContext.ReadValue<Vector2>();
        }

        private void Update_Behaviour_Move() {
            moveDir = SubjectGo.UGo.transform.right * inputVec.x * 3 + SubjectGo.UGo.transform.forward * inputVec.y * 3;
            controller.Move(moveDir * Time.deltaTime);
        }
    }
}