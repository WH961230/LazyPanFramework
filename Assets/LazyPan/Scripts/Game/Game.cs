using UnityEngine;

namespace LazyPan {
    public class Game : MonoBehaviour {
        public bool LoadFinished;
        public Setting Setting => Loader.LoadAsset<Setting>(Loader.AssetType.ASSET, "Setting");

        public static Game Instance;
        private void Awake() {
            Instance = this;
            Setting.InstanceID = 999999;
        }

        private void Update() { Data.Instance.OnUpdateEvent?.Invoke(); }
        private void FixedUpdate() { Data.Instance.OnFixedUpdateEvent?.Invoke(); }
        private void LateUpdate() { Data.Instance.OnLateUpdateEvent?.Invoke(); }
    }
}