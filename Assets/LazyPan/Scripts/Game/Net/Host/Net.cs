using System;
using System.Collections.Generic;

namespace LazyPan {
    public interface INet {
        void AwakeInit(NetBehaviour netBehaviour);
        void StartInit();
        void OnUpdate();
        void OnClear();
    }

    public class Net : NetBehaviour {
        public NetServer NetServer;
        public NetClient NetClient;

        private void Awake() {
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        private void OnUpdate() {
        }

        public override void OnStartClient() {
            base.OnStartClient();
            NetClient = new NetClient(this);
        }

        public override void OnStartServer() {
            base.OnStartServer();
            NetServer = new NetServer(this);
        }
    }
}