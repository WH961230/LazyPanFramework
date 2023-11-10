namespace LazyPan {
    public class Behaviour {
        protected int SubjectID;
        protected int ObjectID;
        protected Go SubjectGo;
        protected Go ObjectGo;

        protected Behaviour(int subjectId, int objectId) {
            SubjectID = subjectId;
            ObjectID = objectId;

            if (SubjectID != -1) {
                SubjectGo = Data.Instance.goDic[SubjectID];
            }
            if (ObjectID != -1) {
                ObjectGo = Data.Instance.goDic[ObjectID];
            }
        }
    }
}