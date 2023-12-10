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
        public void CmdShoot(GameObject selectedObj, Vector3 beginShootVec, Vector3 endShootVec) {
            RpcShoot(selectedObj, beginShootVec, endShootVec);
        }

        [ClientRpc]
        public void RpcShoot(GameObject selectedObj, Vector3 beginShootVec, Vector3 endShootVec) {
            if (selectedObj == null) {
                return;
            }

            selectedObj.transform.parent = Obj.Instance.ObjRoot;
            selectedObj.transform.position = beginShootVec;
            Rigidbody rb = selectedObj.AddComponent<Rigidbody>();
            rb.AddForce((endShootVec - beginShootVec).normalized * 10f, ForceMode.Force);
            Sound.Instance.PlaySound("BubbleShot", beginShootVec, false);
        }
    }
}