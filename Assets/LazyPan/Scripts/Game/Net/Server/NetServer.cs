using UnityEngine;

namespace LazyPan {
    public class NetServer : INet {
        private Net net;
        public void AwakeInit(Net net) {
            this.net = net;
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