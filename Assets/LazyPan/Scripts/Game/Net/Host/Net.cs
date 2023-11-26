using Mirror;
using UnityEngine;

namespace LazyPan {
    public interface INet {
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

        [Command]
        public void CmdShoot(Vector3 beginShootVec, Vector3 endShootVec) {
            Debug.Log("Server - CmdShoot");
            RpcShoot(beginShootVec, endShootVec);
        }

        [ClientRpc]
        public void RpcShoot(Vector3 beginShootVec, Vector3 endShootVec) {
            GameObject bulletGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bulletGo.transform.localScale /= 5;
            bulletGo.transform.position = beginShootVec;
            Rigidbody rb = bulletGo.AddComponent<Rigidbody>();
            rb.AddForce((endShootVec - beginShootVec).normalized * 10f, ForceMode.Impulse);
            Debug.Log("Client - CmdShoot");
        }
    }
}