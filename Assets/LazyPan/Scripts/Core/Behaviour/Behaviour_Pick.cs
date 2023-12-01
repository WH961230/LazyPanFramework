using UnityEngine;

namespace LazyPan {
    public class Behaviour_Pick : Behaviour {
        public Behaviour_Pick(uint subjectId) : base(subjectId) {
            SubjectComp.OnTriggerEnterEvent?.AddListener(OnTriggerEnter);
        }

        private void OnTriggerEnter(Collider collider) {
            string[] strArray = collider.name.Split('_');
            if (strArray.Length < 3) {
                return;
            }
            uint id = uint.Parse(strArray[2]);
            DataBody triggerDataBody = Data.Instance.dataBodyDic[id];
            GoType type = (GoType)triggerDataBody.Type;
            if (type == GoType.PickableObj) {
                if (triggerDataBody.Health > 0) {
                    return;
                }

                string bindBehaviour = ObjConfig.Get(triggerDataBody.GoSign).Behaviour;
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
                
                Data.Instance.AddOwnedDataBody(SubjectID, triggerDataBody);
                triggerDataBody.Go.UGo.SetActive(false);
                Sound.Instance.PlaySound("Pickup", new Sound.SoundInfo(SubjectUGo.transform.position));
            }
        }
    }
}