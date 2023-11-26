using UnityEngine;

namespace LazyPan {
    public class Behaviour {
        protected uint SubjectID;
        protected Go SubjectGo;
        protected GameObject SubjectUGo;
        protected Comp SubjectComp;
        protected bool SubjectIsLocal;
        protected BehaviourData SubjectBehaviourData;

        protected Behaviour(uint subjectId) {
            SubjectID = subjectId;

            if (SubjectID != -1) {
                SubjectGo = Data.Instance.dataBodyDic[SubjectID].Go;
                SubjectUGo = SubjectGo.UGo;
                SubjectComp = SubjectGo.Comp;
                SubjectBehaviourData = Data.Instance.dataBodyDic[SubjectID].BehaviourData;
                SubjectIsLocal = Data.Instance.dataBodyDic[SubjectID].isLocalMainPlayer;
            }
        }
    }

    public class BehaviourData {
        public float GravityValue;
        public bool IsGrounded;
        public bool IsJumping;
        public bool IsFlying;
        public Vector3 MoveVec;
    }
}