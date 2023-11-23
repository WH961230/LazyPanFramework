using Mirror;

namespace LazyPan {
    public class NetManager : NetworkManager {
        public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
            base.OnServerAddPlayer(conn);
        }
    }
}