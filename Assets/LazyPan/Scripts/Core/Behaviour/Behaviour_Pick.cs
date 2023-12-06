using UnityEngine;

namespace LazyPan {
    public class Behaviour_Pick : Behaviour {
        public Behaviour_Pick(uint subjectId) : base(subjectId) {
            SubjectComp.OnTriggerStayEvent?.AddListener(OnTriggerStay);
        }

        private void OnTriggerStay(Collider collider) {
            string[] strArray = collider.name.Split('_');
            if (strArray.Length < 3) {
                return;
            }

            uint id = uint.Parse(strArray[2]);
            Entity triggerEntity = Data.Instance.EntityDic[id];
            GoType type = (GoType)triggerEntity.Type;
            if (type == GoType.PickableObj) {
                if (Data.Instance.TryGetOwnedEntity(SubjectID, triggerEntity)) {
                    return;
                }
            }

            if (type == GoType.PickableObj || type == GoType.PickableBehaviourObj) {
                if (triggerEntity.Health > 0) {
                    return;
                }

                string bindBehaviour = ObjConfig.Get(triggerEntity.GoSign).Behaviour;
                if (!string.IsNullOrEmpty(bindBehaviour)) {
                    string[] behaviourArray = bindBehaviour.Split('|');
                    for (int i = 0; i < behaviourArray.Length; i++) {
                        string behaviourSign = behaviourArray[i];
                        if (BehaviourConfig.Get(behaviourSign).CanFallOff) {
                            Data.Instance.RemoveBehaviour(id, behaviourSign);
                            Data.Instance.AddBehaviour(SubjectID, behaviourSign);
                        }
                    }
                }

                if (type == GoType.PickableObj) {
                    Data.Instance.AddOwnedEntity(SubjectID, triggerEntity);
                    triggerEntity.Go.UGo.transform.SetParent(SubjectComp.Get<Transform>("HandPoint"));
                } else {
                    triggerEntity.Go.UGo.SetActive(false);
                }
                Sound.Instance.PlaySound("Pickup", new Sound.SoundInfo(SubjectUGo.transform.position));
            }
        }
    }
}