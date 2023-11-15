using UnityEngine;

namespace LazyPan {
    public class Behaviour {
        protected int SubjectID;
        protected Go SubjectGo;
        protected GameObject SubjectUGo;
        protected Comp SubjectComp;

        protected Behaviour(int subjectId) {
            SubjectID = subjectId;

            if (SubjectID != -1) {
                SubjectGo = Data.Instance.dataBodyDic[SubjectID].Go;
                SubjectUGo = SubjectGo.UGo;
                SubjectComp = SubjectUGo.GetComponent<Comp>();
            }
        }
    }
}