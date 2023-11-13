namespace LazyPan {
    public class Behaviour {
        protected int SubjectID;
        protected Go SubjectGo;

        protected Behaviour(int subjectId) {
            SubjectID = subjectId;

            if (SubjectID != -1) {
                SubjectGo = Data.Instance.dataBodyDic[SubjectID].Go;
            }
        }
    }
}