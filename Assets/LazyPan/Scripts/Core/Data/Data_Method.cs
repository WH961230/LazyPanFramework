﻿using System;
using System.Reflection;

namespace LazyPan {
    public partial class Data : Singleton<Data> {
        public bool TryGetEntityByID(uint id, out Entity entity) {
            if(EntityDic.TryGetValue(id, out entity)) {
                return true;
            }

            return false;
        }

        public bool TryGetEntityByType(int type, out uint id) {
            foreach (var body in EntityDic.Values) {
                if (type == body.Type) {
                    id = body.ID;
                    return true;
                }
            }

            id = default;
            return false;
        }

        public bool TryGetLocalPlayer(out uint id) {
            foreach (var body in EntityDic.Values) {
                if (body.isLocalMainPlayer) {
                    id = body.ID;
                    return true;
                }
            }

            id = default;
            return false;
        }

        #region Behaviour

        public void AddBehaviour(uint id, string sign) {
            if (!EntityDic.ContainsKey(id) || GetBehaviourIndex(id, sign) != -1) {
                return;
            }

            Entity entity = EntityDic[id];
            Type type = Assembly.Load("Assembly-CSharp").GetType(string.Concat("LazyPan.", sign));
            Behaviour instanceBehaviour = (Behaviour) Activator.CreateInstance(type, id);
            entity.Behaviours.Add(instanceBehaviour);
            if (entity.isLocalMainPlayer) {
                //展示可主动操作的行为
                UI.Instance.OnAddBehaviour?.Invoke(instanceBehaviour);
            }
        }

        public void RemoveBehaviour(uint id, string sign) {
            int index = GetBehaviourIndex(id, sign);
            if (!EntityDic.ContainsKey(id) || index == -1) {
                return;
            }

            EntityDic[id].Behaviours.RemoveAt(index);
        }

        private int GetBehaviourIndex(uint id, string sign) {
            int retIndex = -1;
            if (EntityDic.TryGetValue(id, out Entity entity)) {
                for (int i = 0; i < entity.Behaviours.Count; i++) {
                    if (entity.Behaviours[i].GetType().ToString() == sign) {
                        retIndex = i;
                        break;
                    }
                }
            }

            return retIndex;
        }

        public bool TryGetBehaviourBySign(uint id, string sign, out Behaviour outBehaviour) {
            if (TryGetEntityByID(id, out Entity entity)) {
                foreach (var behaviour in entity.Behaviours) {
                    if (behaviour.GetBehaviourSign() == sign) {
                        outBehaviour = behaviour;
                        return true;
                    }
                }
            }

            outBehaviour = default;
            return false;
        }

        #endregion

        #region OwnedEntity

        public void AddOwnedEntity(uint id, Entity entity) {
            if (!EntityDic.ContainsKey(id) || GetOwnedEntityIndex(id, entity.ID) != -1) {
                return;
            }

            EntityDic[id].OwnedEntities.Add(entity);
            UI.Instance.OnAddOwnedEntity?.Invoke(entity);
        }

        public void RemoveOwnedEntity(uint id, uint ownedEntity) {
            int index = GetOwnedEntityIndex(id, ownedEntity);
            if (!EntityDic.ContainsKey(id) || index == -1) {
                return;
            }

            EntityDic[id].OwnedEntities.RemoveAt(index);
        }

        public int GetOwnedEntityIndex(uint id, uint entityID) {
            int retIndex = -1;
            if (EntityDic.TryGetValue(id, out Entity entity)) {
                for (int i = 0; i < entity.OwnedEntities.Count; i++) {
                    if (entity.OwnedEntities[i].ID == entityID) {
                        retIndex = i;
                        break;
                    }
                }
            }

            return retIndex;
        }

        public bool TryGetOwnedEntity(uint id, Entity entity) {
            if (EntityDic.TryGetValue(id, out Entity subjectEntity)) {
                for (int i = 0; i < subjectEntity.OwnedEntities.Count; i++) {
                    if (subjectEntity.OwnedEntities[i] == entity) {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        public bool GetEntityByInstanceID(int instanceID, out Entity outEntity) {
            foreach (var entity in EntityDic.Values) {
                if (entity.GoInstanceID == instanceID) {
                    outEntity = entity;
                    return true;
                }
            }

            outEntity = default;
            return false;
        }
    }
}