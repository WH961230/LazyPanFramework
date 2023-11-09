using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class Launch : MonoBehaviour {
        [SerializeField] private string sceneName;
        private Transform uiRoot;
        private Game game;

        private void Start() {
            Loader.LoadGo("主相机", "Camera/Camera_Main", null, true);
            Loader.LoadGo("灯光", "Light/Light_Directional", null, true);

            uiRoot = Loader.LoadGo("加载画布", "Global/Global_UI_Root", null, true).transform;
            Comp uiBeginComp = Loader.LoadComp("主界面", "UI/UI_Begin", uiRoot, true);
            Stage stage = uiRoot.gameObject.AddComponent<Stage>();
            DontDestroyOnLoad(uiRoot.gameObject);

            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Start"), stage.Load, sceneName);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Quit"), Act.QuitGame);
        }
    }
}