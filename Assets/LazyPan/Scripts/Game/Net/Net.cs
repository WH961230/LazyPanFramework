using Mirror;
using UnityEngine;

namespace LazyPan {
    public class Net : NetworkManager {
        public override void OnStartClient() {
            base.OnStartClient();
            Debug.Log("OnStartClient");
        }

        public override void OnStartServer() {
            base.OnStartServer();
            Debug.Log("OnStartServer");
        }

        public override void OnStartHost() {
            base.OnStartHost();
            Debug.Log("OnStartHost");
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
            base.OnServerAddPlayer(conn);
            Debug.Log("OnServerAddPlayer " + conn.identity.netId);
        }
    }
}