using System.Collections.Generic;

namespace LazyPan {
    public class NetGlobal : NetBehaviour {
        public NetGlobalServer NetGlobalServer;
        public NetGlobalClient NetGlobalClient;

        private List<INet> loops = new List<INet>();
        private void RequestLoop<T>() where T : INet, new() {
            T loop = new T();
            loop.AwakeInit(this);
            loops.Add(loop);
        }

        private void OnUpdate() {
            for (int i = 0; i < loops.Count; i++) {
                loops[i].OnUpdate();
            }
        }

        public override void OnStartClient() {
            base.OnStartClient();
            if (isClient) {
                RequestLoop<NetGlobalClient>();
                Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
            }
        }

        public override void OnStartServer() {
            base.OnStartServer();
            if (isServer) {
                RequestLoop<NetGlobalServer>();
                Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
            }
        }
    }
}