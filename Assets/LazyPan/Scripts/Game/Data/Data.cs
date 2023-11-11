using System.Collections.Generic;
using UnityEngine.Events;

namespace LazyPan {
    public partial class Data {
        public Dictionary<int, DataBody> dataBodyDic = new Dictionary<int, DataBody>();
        public Dictionary<int, Go> goDic = new Dictionary<int, Go>();
        public UnityEvent OnUpdateEvent = new UnityEvent();
        public UnityEvent OnFixedUpdateEvent = new UnityEvent();
        public UnityEvent OnLateUpdateEvent = new UnityEvent();
    }

    public class DataBody {
        public int GoInstanceID;
        public int ID;
        public Go Go;
        public List<Behaviour> Behaviours = new List<Behaviour>();
    }
}