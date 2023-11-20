using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class Launch : MonoBehaviour {
        private Transform uiRoot;
        private Game game;

        private void Start() {
            Loader.LoadGo("主相机", "Obj/Obj_MainCamera", null, true);
            Loader.LoadGo("灯光", "Obj/Obj_MainDirectionalLight", null, true);

            uiRoot = Loader.LoadGo("加载画布", "Global/Global_UI_Launch_Root", null, true).transform;
            Comp uiBeginComp = Loader.LoadComp("主界面", "UI/UI_Begin", uiRoot, true);
            Stage stage = uiRoot.gameObject.AddComponent<Stage>();
            DontDestroyOnLoad(uiRoot);

            GameObject netRoot = Loader.LoadGo("网络", "Global/Global_Net_Root", null, true);
            DontDestroyOnLoad(netRoot);

            string networkAddress = uiBeginComp.Get<TMP_InputField>("UI_Begin_networkAddress").text;
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Start"), stage.Load, networkAddress);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Quit"), Act.QuitGame);
        }
    }
}