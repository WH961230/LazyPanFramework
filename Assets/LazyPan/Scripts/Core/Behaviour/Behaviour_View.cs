using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_View : Behaviour {
        private Vector2 inputVec;
        private float xRotate = 0.0f;
        private float yRotate = 0.0f;

        public Behaviour_View(int subjectId) : base(subjectId) {
            Input.Instance.Load("Player/InputView", Input_InputView);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_InputView);
        }

        private void Input_InputView(InputAction.CallbackContext callbackContext) {
            inputVec = callbackContext.ReadValue<Vector2>();
            inputVec *= 0.5f;
            inputVec *= 0.1f;
        }

        private void Update_Behaviour_InputView() {
            xRotate -= inputVec.y;
            yRotate += inputVec.x;
            SubjectGo.UGo.transform.rotation = Quaternion.Euler(0, yRotate, 0);
        }
    }
}