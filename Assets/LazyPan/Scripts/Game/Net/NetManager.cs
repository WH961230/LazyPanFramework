using Mirror;

namespace LazyPan {
    public class NetManager : NetworkManager {
        public NetGlobalServer NetGlobalServer;
        public NetGlobalClient NetGlobalClient;
        public static NetManager Instance;

        public override void Awake() {
            base.Awake();
            Instance = this;
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        public override void OnStartServer() {
            base.OnStartServer();
            NetGlobalServer = new NetGlobalServer();
        }

        public override void OnStartClient() {
            base.OnStartClient();
            NetGlobalClient = new NetGlobalClient();
        }

        private void OnUpdate() {
        }
    }
}