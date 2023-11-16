using UnityEngine;

namespace LazyPan {
    public class Behaviour_FollowLook : Behaviour {
        private Transform targetViewPoint;
        private float followLerpSpeed;
        public Behaviour_FollowLook(int subjectId) : base(subjectId) {
            if (Data.Instance.TryGetDataBodyByType((int)GoType.MainPlayer, out int playerId)) {
                targetViewPoint = Data.Instance.dataBodyDic[playerId].Go.Comp.Get<Transform>("ViewPoint");
            }

            string parameter = BehaviourConfig.Get("Behaviour_FollowLook").Parameter;
            followLerpSpeed = float.Parse(parameter);

            Data.Instance.OnLateUpdateEvent?.AddListener(LateUpdate_Behaviour_FollowLook);
        }

        private void LateUpdate_Behaviour_FollowLook() {
            if (targetViewPoint == null) {
                return;
            }
            SubjectUGo.transform.position = Vector3.Lerp(SubjectUGo.transform.position, targetViewPoint.position, Time.deltaTime * followLerpSpeed);
            SubjectUGo.transform.rotation = Quaternion.Lerp(SubjectUGo.transform.rotation, targetViewPoint.rotation, Time.deltaTime * followLerpSpeed);
        }
    }
}