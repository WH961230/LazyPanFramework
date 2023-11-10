using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace LazyPan {
    public partial class Data {
        // public Dictionary<int, int> goID = new Dictionary<int, int>();//key:GetInstanceID value:ID 
        // public Dictionary<int, Go> go = new Dictionary<int, Go>();//key:ID value:Go
        // public Dictionary<int, List<Behaviour>> goFunc = new Dictionary<int, List<Behaviour>>();//key:ID value:Behaviour
        public Dictionary<int, DataBody> dataBodyDic = new Dictionary<int, DataBody>();
        public Dictionary<int, Go> goDic = new Dictionary<int, Go>();
        public UnityEvent OnUpdateEvent;
        public UnityEvent OnFixedUpdateEvent;
        public UnityEvent OnLateUpdateEvent;
    }

    public class DataBody {
        public int GoInstanceID;
        public int ID;
        public Go Go;
        public List<Behaviour> Behaviours = new List<Behaviour>();
    }
}