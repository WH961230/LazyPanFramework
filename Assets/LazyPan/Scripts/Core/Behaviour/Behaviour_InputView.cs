using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_InputView : Behaviour {
        private Vector2 inputVec;
        private float xRotate = 0.0f;
        private float yRotate = 0.0f;

        public Behaviour_InputView(int subjectId, int objectId) : base(subjectId, objectId) {
            Input.Instance.Load("Player/InputView", Input_InputView);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_InputView);
        }

        private void Input_InputView(InputAction.CallbackContext callbackContext) {
            inputVec = callbackContext.ReadValue<Vector2>();
            inputVec *= 0.5f; // Account for scaling applied directly in Windows code by old input system.
            inputVec *= 0.1f; // Account for sensitivity setting on old Mouse X and Y axes.
            Debug.LogFormat("x:{0} y:{1}", inputVec.x, inputVec.y);
            Debug.LogFormat("oldx : {0} oldy: {1}", UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
        }

        private void Update_Behaviour_InputView() {
            xRotate -= inputVec.y;
            yRotate += inputVec.x;
            SubjectGo.UGo.transform.rotation = Quaternion.Euler(0, yRotate, 0);
        }
    }
}