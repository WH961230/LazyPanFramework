using Cinemachine;
using UnityEngine;

namespace LazyPan {
    public class Behaviour_FreeLook : Behaviour {
        private GameObject targetUgo;

        public Behaviour_FreeLook(int subjectId) : base(subjectId) {
            if (Data.Instance.TryGetDataBodyByType((int)GoType.Camera, out int cameraId)) {
                targetUgo = Data.Instance.dataBodyDic[cameraId].Go.UGo;

                targetUgo.AddComponent<CinemachineBrain>();
                CinemachineFreeLook cinemachineFreeLook = targetUgo.AddComponent<CinemachineFreeLook>();
                cinemachineFreeLook.LookAt = targetUgo.transform;
                cinemachineFreeLook.Follow = targetUgo.transform;
            }
        }
    }
}