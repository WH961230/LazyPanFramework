using UnityEngine;

namespace LazyPan {
    public class Behaviour {
        protected int SubjectID;
        protected Go SubjectGo;
        protected GameObject SubjectUGo;
        protected Comp SubjectComp;
        protected BehaviourData SubjectBehaviourData;

        protected Behaviour(int subjectId) {
            SubjectID = subjectId;

            if (SubjectID != -1) {
                SubjectGo = Data.Instance.dataBodyDic[SubjectID].Go;
                SubjectUGo = SubjectGo.UGo;
                SubjectComp = SubjectGo.Comp;
                SubjectBehaviourData = Data.Instance.dataBodyDic[SubjectID].BehaviourData;
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