using UnityEngine;

namespace LazyPan {
    public class NetClient : INet {
        private Net net;
        public void AwakeInit(Net net) {
            this.net = net;
            this.net.NetClient = this;
        }

        public void StartInit() {
        }

        public void OnUpdate() {
            Debug.Log("NetClient OnUpdate");
        }

        public void OnClear() {
        }
    }
}