using UnityEngine;

namespace LazyPan {
    public class Game : MonoBehaviour {
        public bool LoadFinished;
        public Transform GameRoot => transform;
        public Setting Setting => Loader.LoadAsset<Setting>(Loader.AssetType.ASSET, "Setting");

        public static Game Instance;
        private void Awake() {
            Instance = this;
        }

        private void Start() {
            Init();
            Load();
        }

        private void Update() { UpdateEvent(); }
        private void FixedUpdate() { FixedUpdateEvent(); }
        private void LateUpdate() { LateUpdateEvent(); }

        private void Init() {
            Config.Instance.Init();
            new Message();
            new Data();
            new Input();
            new UI();
            new Obj();
            new Behaviour();
        }

        private void Load() {
            UI.Instance.Load();
            Obj.Instance.Load();
            LoadFinished = true;
        }

        private void UpdateEvent() { Data.Instance.OnUpdateEvent?.Invoke(); }
        private void FixedUpdateEvent() { Data.Instance.OnFixedUpdateEvent?.Invoke(); }
        private void LateUpdateEvent() { Data.Instance.OnLateUpdateEvent?.Invoke(); }
    }
}