using System.Collections.Generic;
using UnityEngine.Events;

namespace LazyPan {
    public partial class Data {
        public Dictionary<int, DataBody> dataBodyDic = new Dictionary<int, DataBody>();
        public UnityEvent OnUpdateEvent = new UnityEvent();
        public UnityEvent OnFixedUpdateEvent = new UnityEvent();
        public UnityEvent OnLateUpdateEvent = new UnityEvent();
    }

    public class DataBody {
        public int GoInstanceID;
        public int ID;
        public Go Go;
        public int Type;
        public List<Behaviour> Behaviours = new List<Behaviour>();
    }
}