using UnityEngine;

namespace LazyPan {
    public class Behaviour_Follow : Behaviour {
        private GameObject targetUGo;
        private readonly int goTypeIndex;
        public Behaviour_Follow(int subjectId) : base(subjectId) {
            goTypeIndex = int.Parse(BehaviourConfig.Get("Behaviour_Follow").Parameter);
            if (Data.Instance.TryGetDataBodyByType(goTypeIndex, out int id)) {
                targetUGo = Data.Instance.dataBodyDic[id].Go.UGo;
            }
            Data.Instance.OnLateUpdateEvent?.AddListener(LateUpdate_Behaviour_Follow);
        }

        private void LateUpdate_Behaviour_Follow() {
            if (targetUGo) {
                SubjectGo.UGo.transform.position = targetUGo.transform.position + Vector3.up * 3 - targetUGo.transform.forward * 3;
            } else {
                if (Data.Instance.TryGetDataBodyByType(goTypeIndex, out int id)) {
                    targetUGo = Data.Instance.dataBodyDic[id].Go.UGo;
                }
            }
        }
    }
}