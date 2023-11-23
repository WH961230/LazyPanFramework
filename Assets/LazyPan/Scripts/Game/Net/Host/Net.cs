using System.Collections.Generic;

namespace LazyPan {
    public interface INet {
        void AwakeInit(Net net);
        void StartInit();
        void OnUpdate();
        void OnClear();
    }

    public class Net : NetBehaviour {
        public NetServer NetServer;
        public NetClient NetClient;

        private List<INet> loops = new List<INet>();
        protected override void AwakeInit() {
            base.AwakeInit();
            if (NetManager.ConnectType == ConnectType.Server) {
                RequestLoop<NetServer>();
            }

            if (NetManager.ConnectType == ConnectType.Client) {
                RequestLoop<NetClient>();
            }
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

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
            if (NetManager.IsClient && isLocalPlayer) {
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
        }

        public override void OnStartLocalPlayer() {
            base.OnStartLocalPlayer();
        }

        public override void OnStartAuthority() {
            base.OnStartAuthority();
        }
    }
}