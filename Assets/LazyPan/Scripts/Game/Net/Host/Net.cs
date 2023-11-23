using System.Collections.Generic;

namespace LazyPan {
    public interface INet {
        void AwakeInit(NetBehaviour netBehaviour);
        void StartInit();
        void OnUpdate();
        void OnClear();
    }

    public class Net : NetBehaviour {
        public NetServer NetServer;
        public NetClient NetClient;

        private List<INet> loops = new List<INet>();
        private void RequestLoop<T>() where T : INet, new() {
            T loop = new T();
            loop.AwakeInit(this);
            loops.Add(loop);
        }

        private void OnUpdate() {
            for (int i = 0; i < loops.Count; i++) {
                loops[i].OnUpdate();
            }
        }

        public override void OnStartClient() {
            base.OnStartClient();
            if (isClient) {
                RequestLoop<NetClient>();
                Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
            }

            if (isClient && isLocalPlayer) {
                DataBody dataBody = new DataBody();
                string objSign = GetComponent<Comp>().ObjSign;
                dataBody.Go = new Go(dataBody.ID, objSign, gameObject);
                dataBody.GoSign = objSign;
                dataBody.GoInstanceID = dataBody.Go.UGo.GetInstanceID();
                ObjConfig config = ObjConfig.Get(objSign);
                dataBody.Type = config.Type;
                dataBody.Behaviours = new List<Behaviour>();
                Data.Instance.dataBodyDic.TryAdd(dataBody.ID, dataBody);
                Obj.Instance.AddBehaviourFromConfig(dataBody.ID, objSign);
            }
        }

        public override void OnStartServer() {
            base.OnStartServer();
            if (isServer) {
                RequestLoop<NetServer>();
                Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
            }
        }
    }
}