using System;
using UnityEngine;

namespace LazyPan {
    public class Game : MonoBehaviour {
        public bool LoadFinished;
        public Transform GameRoot => transform;
        public Setting Setting => Loader.Load<Setting>(Loader.LoaderType.ASSET, "Setting");

        public static Game Instance;
        private void Awake() {
            Instance = this;
        }

        private void Start() {
            Init();
            Load();
        }

        private void Update() {
            UpdateEvent();
        }

        private void Init() {
            new Message();
            new Data();
            new Input();
            new UI();
            new Obj();
            new Behaviour();
        }

        private void Load() {
            Input.Instance.Load("Setting", Act.OpenSetting);
            UI.Instance.Load();
            Obj.Instance.Load();
            LoadFinished = true;
        }

        private void UpdateEvent() {
            
        }
    }
}