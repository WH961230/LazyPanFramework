using Cinemachine;
using UnityEngine;

namespace LazyPan {
    public class Behaviour_FreeLook : Behaviour {
        private GameObject targetUgo;

        public Behaviour_FreeLook(int subjectId) : base(subjectId) {
            SubjectGo.UGo.AddComponent<CinemachineBrain>();
            CinemachineFreeLook cinemachineFreeLook = SubjectGo.UGo.AddComponent<CinemachineFreeLook>();
            if (Data.Instance.TryGetDataBodyByType((int)GoType.MainPlayer, out int ID)) {
                targetUgo = Data.Instance.dataBodyDic[ID].Go.UGo;
                cinemachineFreeLook.LookAt = targetUgo.transform;
                cinemachineFreeLook.Follow = targetUgo.transform;
            }
        }
    }
}