namespace LazyPan {
    public class NetClient {
        private Net Net;
        public NetClient(Net net) {
            Net = net;
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
            Obj.Instance.LoadObj(Net.netId, Net.isLocalPlayer, Net.gameObject);
        }

        public void OnUpdate() {
        }

        public void OnClear() {
        }
    }
}