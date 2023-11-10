using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Move : Behaviour {
        private Vector2 inputVec;

        public Behaviour_Move(int subjectId, int objectId) : base(subjectId, objectId) {
            Input.Instance.Load("Player/Move", Input_Move);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Move);
        }

        private void Input_Move(InputAction.CallbackContext callbackContext) {
            inputVec = callbackContext.ReadValue<Vector2>();
        }

        private void Update_Behaviour_Move() {
            SubjectGo.UGo.transform.position += SubjectGo.UGo.transform.forward * inputVec.y * Time.deltaTime * 3;
            SubjectGo.UGo.transform.position += SubjectGo.UGo.transform.right * inputVec.x * Time.deltaTime * 3;
        }
    }
}