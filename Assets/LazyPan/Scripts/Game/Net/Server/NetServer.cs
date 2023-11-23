using UnityEngine;

namespace LazyPan {
    public class NetServer : INet {
        private Net net;
        public void AwakeInit(NetBehaviour netBehaviour) {
            this.net = (Net)netBehaviour;
            this.net.NetServer = this;
        }

        public void StartInit() {
        }

        public void OnUpdate() {
            Debug.Log("NetServer OnUpdate");
        }

        public void OnClear() {
        }
    }
}