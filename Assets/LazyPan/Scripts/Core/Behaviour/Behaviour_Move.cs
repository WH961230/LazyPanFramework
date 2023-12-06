using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Move : Behaviour {
        private Vector2 moveInputVec;
        private Vector2 viewInputVec;
        private Vector3 moveDir;
        private float yRotate = 0.0f;
        private float xRotate = 0.0f;
        private GameObject Head;
        private CharacterController controller;

        public Behaviour_Move(uint subjectId) : base(subjectId) {
            Input.Instance.Load("Player/Move", Input_Move);
            Input.Instance.Load("Player/View", Input_View);
            controller = SubjectComp.Get<CharacterController>("CharacterController");
            Head = SubjectComp.Get<GameObject>("Head");
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Move);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_View);
        }

        private void Input_Move(InputAction.CallbackContext callbackContext) {
            moveInputVec = callbackContext.ReadValue<Vector2>();
        }

        private void Input_View(InputAction.CallbackContext callbackContext) {
            viewInputVec = callbackContext.ReadValue<Vector2>();
            viewInputVec *= 0.5f;
            viewInputVec *= 0.1f;
        }

        private void Update_Behaviour_Move() {
            if (!controller) {
                return;
            }

            moveDir = SubjectGo.UGo.transform.right * moveInputVec.x * 3 + SubjectGo.UGo.transform.forward * moveInputVec.y * 3;
            controller.Move(moveDir * Time.deltaTime);
        }

        private void Update_Behaviour_View() {
            if (!Head) {
                return;
            }

            yRotate += viewInputVec.x;
            xRotate -= viewInputVec.y;
            SubjectUGo.transform.rotation = Quaternion.Euler(0, yRotate, 0);
            Head.transform.rotation = Quaternion.Euler(xRotate, Head.transform.rotation.eulerAngles.y, 0);
        }
    }
}