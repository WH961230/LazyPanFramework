using System.Collections.Generic;
using UnityEngine.Events;

namespace LazyPan {
    public partial class Data {
        public Dictionary<uint, DataBody> dataBodyDic = new Dictionary<uint, DataBody>();
        public UnityEvent OnUpdateEvent = new UnityEvent();
        public UnityEvent OnFixedUpdateEvent = new UnityEvent();
        public UnityEvent OnLateUpdateEvent = new UnityEvent();
    }

    public class DataBody {
        public int GoInstanceID;
        public uint ID;
        public string GoSign;
        public Go Go;
        public int Type;
        public bool isLocalMainPlayer;
        public int Health;
        public BehaviourData BehaviourData = new BehaviourData();
        public List<Behaviour> Behaviours = new List<Behaviour>();
    }
}