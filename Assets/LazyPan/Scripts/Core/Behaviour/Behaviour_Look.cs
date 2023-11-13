using UnityEngine;

namespace LazyPan {
    public class Behaviour_Look : Behaviour {
        private GameObject targetUGo;
        private readonly int goTypeIndex;
        public Behaviour_Look(int subjectId) : base(subjectId) {
            goTypeIndex = int.Parse(BehaviourConfig.Get("Behaviour_Look").Parameter);
            if (Data.Instance.TryGetDataBodyByType(goTypeIndex, out int id)) {
                targetUGo = Data.Instance.dataBodyDic[id].Go.UGo;
            }
            Data.Instance.OnLateUpdateEvent?.AddListener(LateUpdate_Behaviour_Look);
        }

        private void LateUpdate_Behaviour_Look() {
            if (targetUGo) {
                SubjectGo.UGo.transform.LookAt(targetUGo.transform.position + Vector3.up * 1);
            } else {
                if (Data.Instance.TryGetDataBodyByType(goTypeIndex, out int id)) {
                    targetUGo = Data.Instance.dataBodyDic[id].Go.UGo;
                }
            }
        }
    }
}