using UnityEngine;

namespace LazyPan {
    public class Behaviour_Look : Behaviour {
        public Behaviour_Look(int subjectId, int objectId) : base(subjectId, objectId) {
            Data.Instance.OnLateUpdateEvent?.AddListener(LateUpdate_Behaviour_Look);
        }

        private void LateUpdate_Behaviour_Look() {
            SubjectGo.UGo.transform.LookAt(ObjectGo.UGo.transform.position + Vector3.up * 1);
        }
    }
}