using Mirror;
using UnityEngine;

namespace LazyPan {
    public class NetClient {
        private Net Net;

        public NetClient(Net net) {
            Net = net;
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        public void OnUpdate() {
            if (Game.Instance.LoadFinished && !Data.Instance.EntityDic.ContainsKey(Net.netId)) {
                Obj.Instance.LoadObj(Net.netId, Net.isLocalPlayer, Net.gameObject);
            }
        }

        public void OnClear() {
        }
    }
}