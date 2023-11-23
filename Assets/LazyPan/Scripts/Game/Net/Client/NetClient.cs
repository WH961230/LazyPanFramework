using UnityEngine;

namespace LazyPan {
    public class NetClient : INet {
        private Net net;
        public void AwakeInit(NetBehaviour netBehaviour) {
            this.net = (Net)netBehaviour;
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