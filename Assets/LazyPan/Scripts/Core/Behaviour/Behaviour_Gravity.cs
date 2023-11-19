using UnityEngine;

namespace LazyPan {
    public class Behaviour_Gravity : Behaviour {
        private CharacterController controller;
        private Collider[] colliders;
        private float OverlapCapsuleOffset;
        private LayerMask GravityDetectMaskLayer;

        public Behaviour_Gravity(int subjectId) : base(subjectId) {
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Gravity);
            Comp goComp = SubjectUGo.GetComponent<Comp>();
            controller = goComp.Get<CharacterController>("CharacterController");
            OverlapCapsuleOffset = -0.1f;
#if UNITY_EDITOR
            goComp.OnDrawGizmosAction.AddListener(Gizmos_Gravity);
#endif
            GravityDetectMaskLayer = 1 << LayerMask.NameToLayer("Terrain");
            SubjectBehaviourData.GravityValue = -9.8f;
        }

        private void Update_Behaviour_Gravity() {
            if (controller == null) {
                return;
            }

            GroundCheck();
            if (SubjectBehaviourData.IsGrounded) {
                if (!SubjectBehaviourData.IsJumping && !SubjectBehaviourData.IsFlying) {
                    SubjectBehaviourData.MoveVec.y = 0;
                }
            } else {
                SubjectBehaviourData.MoveVec.y += SubjectBehaviourData.GravityValue * Time.deltaTime;
            }

            if (controller != null) {
                controller.Move(SubjectBehaviourData.MoveVec * Time.deltaTime);
            }
        }

        private void GroundCheck() {
            if (controller != null) {
                Vector3 bottom = SubjectUGo.transform.position + SubjectUGo.transform.up * controller.radius + SubjectUGo.transform.up * OverlapCapsuleOffset;
                Vector3 top = SubjectUGo.transform.position + SubjectUGo.transform.up * controller.height - SubjectUGo.transform.up * controller.radius;
                colliders = Physics.OverlapCapsule(bottom, top, controller.radius, GravityDetectMaskLayer);
                SubjectBehaviourData.IsGrounded = colliders.Length > 0;
            }
        }

#if UNITY_EDITOR
        private void Gizmos_Gravity() {
            if (controller != null) {
                Color buttom = SubjectBehaviourData.IsGrounded ? Color.green : Color.red;
                buttom.a = 0.2f;
                Gizmos.color = buttom;
                Gizmos.DrawSphere(
                    SubjectUGo.transform.position + SubjectUGo.transform.up * controller.radius +
                    SubjectUGo.transform.up * OverlapCapsuleOffset, controller.radius);
            }
        }
#endif
    }
}