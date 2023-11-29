﻿using UnityEngine;

namespace LazyPan {
    public class Behaviour_Pick : Behaviour {
        public Behaviour_Pick(uint subjectId) : base(subjectId) {
            SubjectComp.OnTriggerEnterEvent?.AddListener(OnTriggerEnter);
        }

        private void OnTriggerEnter(Collider collider) {
            string[] strArray = collider.name.Split('_');
            uint id = uint.Parse(strArray[2]);
            GoType type = (GoType)Data.Instance.dataBodyDic[id].Type;
            if (type == GoType.PickableObj) {
                DataBody body = Data.Instance.dataBodyDic[id];
                string bindBehaviour = ObjConfig.Get(body.GoSign).Behaviour;
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

                collider.gameObject.SetActive(false);
            }
        }
    }
}