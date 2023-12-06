using System;
using System.Reflection;

namespace LazyPan {
    public partial class Data : Singleton<Data> {
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

            Type type = Assembly.Load("Assembly-CSharp").GetType(string.Concat("LazyPan.", sign));
            EntityDic[id].Behaviours.Add((Behaviour) Activator.CreateInstance(type, id));
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

        private int GetOwnedEntityIndex(uint id, uint entityID) {
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

        #endregion

        public Entity GetEntityByInstanceID(int instanceID) {
            foreach (var entity in EntityDic.Values) {
                if (entity.GoInstanceID == instanceID) {
                    return entity;
                }
            }

            return null;
        }
    }
}