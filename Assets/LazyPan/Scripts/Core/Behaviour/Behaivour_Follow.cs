using UnityEngine;

namespace LazyPan {
    public class Behaivour_Follow : Behaviour {
        public Behaivour_Follow(int subjectId, int objectId) : base(subjectId, objectId) {
            Data.Instance.OnLateUpdateEvent?.AddListener(LateUpdate_Behaviour_Follow);
        }

        private void LateUpdate_Behaviour_Follow() {
            SubjectGo.UGo.transform.position = ObjectGo.UGo.transform.position + Vector3.up * 3 - ObjectGo.UGo.transform.forward * 3;
        }
    }
}