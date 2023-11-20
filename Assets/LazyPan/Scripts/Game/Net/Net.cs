using Mirror;
using UnityEngine;

namespace LazyPan {
    public class Net : NetworkManager {
        public override void OnStartServer() {
            base.OnStartServer();
            int mainPlayerId = Obj.Instance.LoadObj("Obj_MainPlayer");
            GameObject uGo = Data.Instance.dataBodyDic[mainPlayerId].Go.UGo;
            OnServerAddPlayer(NetworkServer.localConnection, uGo);
        }
    }
}