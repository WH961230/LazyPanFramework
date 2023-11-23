using UnityEngine;

namespace LazyPan {
    public class Game : MonoBehaviour {
        public bool LoadFinished;
        public Transform GameRoot => transform;
        public Setting Setting => Loader.LoadAsset<Setting>(Loader.AssetType.ASSET, "Setting");

        public static Game Instance;
        private void Awake() {
            Instance = this;
            Setting.InstanceID = 0;
        }

        private void Start() {
            Init();
            Load();
        }

        private void Update() { Data.Instance.OnUpdateEvent?.Invoke(); }
        private void FixedUpdate() { Data.Instance.OnFixedUpdateEvent?.Invoke(); }
        private void LateUpdate() { Data.Instance.OnLateUpdateEvent?.Invoke(); }

        private void Init() {
            Config.Instance.Init();
            UI.Instance.Init();
            Obj.Instance.Init();
        }

        private void Load() {
            UI.Instance.Load();
            Obj.Instance.Load();
            LoadFinished = true;
        }
    }
}