using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class Launch : MonoBehaviour {
        private void Start() {
            Loader.LoadGo("主相机", "Obj/Obj_MainCamera", null, true);
            Loader.LoadGo("灯光", "Obj/Obj_MainDirectionalLight", null, true);

            Transform uiRoot = Loader.LoadGo("加载画布", "Global/Global_UI_Launch_Root", null, true).transform;
            Comp uiBeginComp = Loader.LoadComp("主界面", "UI/UI_Begin", uiRoot, true);
            Stage stage = uiRoot.gameObject.AddComponent<Stage>();
            DontDestroyOnLoad(uiRoot);

            GameObject netRoot = Loader.LoadGo("网络", "Global/Global_Net_Root", null, true);
            DontDestroyOnLoad(netRoot);

            NetManager.singleton.networkAddress = uiBeginComp.Get<TMP_InputField>("UI_Begin_networkAddress").text;
            uiBeginComp.Get<TMP_InputField>("UI_Begin_networkAddress").onValueChanged.AddListener(ChangeNetworkAddress);

            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Host"), stage.Load, ConnectType.Host);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Client"), stage.Load, ConnectType.Client);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Server"), stage.Load, ConnectType.Server);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Quit"), Act.QuitGame);
        }

        private void ChangeNetworkAddress(string networkAddress) {
            NetManager.singleton.networkAddress = networkAddress;
        }
    }
}