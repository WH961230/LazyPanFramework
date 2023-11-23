using System;
using System.Reflection;

namespace LazyPan {
    public partial class Data : Singleton<Data> {
        public bool TryGetDataBodyByType(int type, out uint id) {
            foreach (var body in dataBodyDic.Values) {
                if (type == body.Type) {
                    id = body.ID;
                    return true;
                }
            }

            id = default;
            return false;
        }

        public void AddBehaviour(uint id, string sign) {
            if (!dataBodyDic.ContainsKey(id) || GetBehaviourIndex(id, sign) != -1) {
                return;
            }

            Type type = Assembly.Load("Assembly-CSharp").GetType(string.Concat("LazyPan.", sign));
            dataBodyDic[id].Behaviours.Add((Behaviour) Activator.CreateInstance(type, id));
        }

        public void RemoveBehaviour(uint id, string sign) {
            int index = GetBehaviourIndex(id, sign);
            if (!dataBodyDic.ContainsKey(id) || index == -1) {
                return;
            }

            dataBodyDic[id].Behaviours.RemoveAt(index);
        }

        private int GetBehaviourIndex(uint id, string sign) {
            int retIndex = -1;
            if (dataBodyDic.TryGetValue(id, out DataBody body)) {
                for (int i = 0; i < body.Behaviours.Count; i++) {
                    if (body.Behaviours[i].GetType().ToString() == sign) {
                        retIndex = i;
                        break;
                    }
                }
            }

            return retIndex;
        }
    }
}