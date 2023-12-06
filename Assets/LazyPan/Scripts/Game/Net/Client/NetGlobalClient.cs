namespace LazyPan {
    public class NetGlobalClient {
        public bool ActiveGlobalClient;

        public NetGlobalClient() {
            Data.Instance.OnUpdateEvent?.AddListener(OnUpdate);
        }

        public void OnInit() {
            Config.Instance.Init();
            UI.Instance.Init();
            Obj.Instance.Init();
            LoadClientObj();
            ActiveGlobalClient = true;
        }

        public void OnUpdate() {
        }

        public void OnClear() {
        }

        public void LoadClientObj() {
            Obj.Instance.LoadSignObj("Obj_MainTerrain");
            Obj.Instance.LoadSignObj("Obj_MainDirectionalLight");
            Obj.Instance.LoadSignObj("Obj_MainVolume");
            Obj.Instance.LoadSignObj("Obj_MainCamera");
            UI.Instance.Open("UI_Main");
            ;
        }
    }
}