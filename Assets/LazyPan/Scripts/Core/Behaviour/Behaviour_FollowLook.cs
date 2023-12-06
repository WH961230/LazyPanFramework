using UnityEngine;

namespace LazyPan {
    public class Behaviour_FollowLook : Behaviour {
        private Transform targetViewPoint;
        private float followLerpSpeed;

        public Behaviour_FollowLook(uint subjectId) : base(subjectId) {
            string parameter = BehaviourConfig.Get("Behaviour_FollowLook").Parameter;
            followLerpSpeed = float.Parse(parameter);
            Data.Instance.OnLateUpdateEvent?.AddListener(LateUpdate_Behaviour_FollowLook);
        }

        private void LateUpdate_Behaviour_FollowLook() {
            if (targetViewPoint == null) {
                if (Data.Instance.TryGetLocalPlayer(out uint playerId)) {
                    targetViewPoint = Data.Instance.EntityDic[playerId].Go.Comp.Get<Transform>("ViewPoint");
                }

                return;
            }

            SubjectUGo.transform.position = Vector3.Lerp(SubjectUGo.transform.position, targetViewPoint.position, Time.deltaTime * followLerpSpeed);
            SubjectUGo.transform.rotation = Quaternion.Lerp(SubjectUGo.transform.rotation, targetViewPoint.rotation, Time.deltaTime * followLerpSpeed);
        }
    }
}