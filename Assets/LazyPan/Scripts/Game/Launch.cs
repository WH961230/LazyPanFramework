using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class Launch : MonoBehaviour {
        [SerializeField] private string sceneName;
        private Transform uiRoot;
        private Game game;

        private void Start() {
            Loader.Load("主相机", "Camera/Camera_Main", null);
            Loader.Load("灯光", "Light/Light_Directional", null);

            uiRoot = Loader.Load("加载画布", "Global/Global_UI_Root", null).transform;
            Comp uiBeginComp = Loader.LoadComp("主界面", "UI/UI_Begin", uiRoot);
            Stage stage = uiRoot.gameObject.AddComponent<Stage>();
            DontDestroyOnLoad(uiRoot.gameObject);

            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Start"), stage.Load, sceneName);
            Listener.AddListener(uiBeginComp.Get<Button>("UI_Begin_Quit"), Act.QuitGame);
        }
    }
}