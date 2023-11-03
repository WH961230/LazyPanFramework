using UnityEngine.Events;

namespace LazyPan {
    public partial class Data {
        public static Data Instance;
        public Data() {
            Instance = this;
            OnUpdateEvent = new UnityEvent();
            OnFixedUpdateEvent = new UnityEvent();
            OnLateUpdateEvent = new UnityEvent();
        }
    }
}