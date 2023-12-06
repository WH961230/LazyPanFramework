using Mirror;
using UnityEngine;

namespace LazyPan {
    public class NetGlobalServer {
        public bool ActiveGlobalServer;

        public NetGlobalServer() {
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        public void OnInit() {
            Config.Instance.Init();
            ServerSpawnPrefab();
            ActiveGlobalServer = true;
        }

        public void OnUpdate() {
        }

        public void OnClear() {
        }

        private void ServerSpawnPrefab() {
            for (int i = 0; i < NetManager.singleton.spawnPrefabs.Count; i++) {
                GameObject plant = Object.Instantiate(NetManager.singleton.spawnPrefabs[i]);
                NetworkServer.Spawn(plant);
            }
        }
    }
}