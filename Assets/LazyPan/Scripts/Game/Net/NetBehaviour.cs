using Mirror;

namespace LazyPan {
    public class NetBehaviour : NetworkBehaviour {
        private void Awake() {
            AwakeInit();
        }

        protected virtual void AwakeInit() {
        }
    }
}