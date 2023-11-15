using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Jump : Behaviour {
        private GameObject uGo;
        private Vector3 moveDir;
        private CharacterController controller;
        private bool isJumping;
        private bool isGrounded;
        private float GravityValue;
        private float JumpHeight;
        private Collider[] colliders;
        private float OverlapCapsuleOffset;
        private LayerMask GravityDetectMaskLayer;

        public Behaviour_Jump(int subjectId) : base(subjectId) {
            Input.Instance.Load("Player/Jump", Input_Jump);
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Jump);
            uGo = SubjectGo.UGo;
            Comp goComp = uGo.GetComponent<Comp>();
            controller = goComp.Get<CharacterController>("CharacterController");
            OverlapCapsuleOffset = -0.1f;
            isJumping = false;
            goComp.OnDrawGizmosAction.AddListener(Gizmos_Gravity);
            GravityDetectMaskLayer = 1 << LayerMask.NameToLayer("Terrain");
            string[] parameter = BehaviourConfig.Get("Behaviour_Jump").Parameter.Split('|');
            JumpHeight = float.Parse(parameter[0]);
            GravityValue = float.Parse(parameter[1]);
        }

        private void Input_Jump(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed && isGrounded && !isJumping) {
                moveDir.y += Mathf.Sqrt(JumpHeight * -3.0f * GravityValue);
                isJumping = true;
            }
        }

        private void Update_Behaviour_Jump() {
            GroundCheck();
            if (isGrounded) {
                if (!isJumping) {
                    moveDir.y = 0f;
                }
            } else {
                moveDir.y += GravityValue * Time.deltaTime;
                if (moveDir.y < 0) {
                    isJumping = false;
                }
            }

            if (controller != null) {
                controller.Move(moveDir * Time.deltaTime);
            }
        }

        private void GroundCheck() {
            Vector3 bottom = uGo.transform.position + uGo.transform.up * controller.radius + uGo.transform.up * OverlapCapsuleOffset;
            Vector3 top = uGo.transform.position + uGo.transform.up * controller.height - uGo.transform.up * controller.radius;
            colliders = Physics.OverlapCapsule(bottom, top, controller.radius, GravityDetectMaskLayer);
            isGrounded = colliders.Length > 0;
        }

#if UNITY_EDITOR
        private void Gizmos_Gravity() {
            Color buttom = isGrounded ? Color.green : Color.red;
            buttom.a = 0.2f;
            Gizmos.color = buttom;
            Gizmos.DrawSphere(uGo.transform.position + uGo.transform.up * controller.radius + uGo.transform.up * OverlapCapsuleOffset, controller.radius);
        }
#endif
    }
}