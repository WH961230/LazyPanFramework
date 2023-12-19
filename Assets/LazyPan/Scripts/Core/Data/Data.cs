using System.Collections.Generic;
using UnityEngine.Events;

namespace LazyPan {
    public partial class Data {
        public Dictionary<uint, Entity> EntityDic = new Dictionary<uint, Entity>();
        public UnityEvent OnUpdateEvent = new UnityEvent();
        public UnityEvent OnFixedUpdateEvent = new UnityEvent();
        public UnityEvent OnLateUpdateEvent = new UnityEvent();
    }
}