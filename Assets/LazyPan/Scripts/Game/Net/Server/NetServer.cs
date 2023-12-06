namespace LazyPan {
    public class NetServer {
        private Net Net;

        public NetServer(Net net) {
            Net = net;
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        public void OnUpdate() {
        }

        public void OnClear() {
        }
    }
}