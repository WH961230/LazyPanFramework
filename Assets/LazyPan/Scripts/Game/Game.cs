using UnityEngine;

namespace LazyPan {
    public class Game : MonoBehaviour {
        public Transform GameRoot => transform;

        public static Game Instance;
        private void Awake() {
            Instance = this;
        }

        private void Start() {
            Init();
            Load();
        }

        private void Init() {
            new Message();
            new Input().Start();
            new UI().Start();
            new Obj().Start();
        }

        private void Load() {
            Input.Instance.Load();
            UI.Instance.Load();
            Obj.Instance.Load();
        }
    }
}