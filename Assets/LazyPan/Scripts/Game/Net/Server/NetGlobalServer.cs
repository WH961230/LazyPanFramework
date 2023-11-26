using Mirror;
using UnityEngine;

namespace LazyPan {
    public class NetGlobalServer {
        private bool SpawnPrefabFinish;
        public NetGlobalServer() {
            Config.Instance.Init();
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        public void OnUpdate() {
            if (!SpawnPrefabFinish) {
                if (NetworkServer.active && Game.Instance && Game.Instance.LoadFinished) {
                    ServerSpawnPrefab();
                    SpawnPrefabFinish = true;
                }
            }
        }

        public void OnClear() {
        }

        private void ServerSpawnPrefab() {
            Debug.Log("ServerSpawnPrefab");
            for (int i = 0; i < NetManager.singleton.spawnPrefabs.Count; i++) {
                GameObject plant = Object.Instantiate(NetManager.singleton.spawnPrefabs[i]);
                NetworkServer.Spawn(plant);
            }
        }
    }
}