using LazyPan.Scripts.Core.Component;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LazyPan.Scripts.Game {
    public class Game : MonoBehaviour {
        public Transform uiRoot;
        public PlayerInput playerInput;

        private void Awake() {
            uiRoot = Loader.Loader.Load("画布", "Interface/UI_Canvas", null).transform;
            playerInput.actions["Setting"].performed += OpenSetting;
        }

        private void OpenSetting(InputAction.CallbackContext callbackContext) {
            GameComp comp = Loader.Loader.LoadComp("设置界面", "Interface/UI_Setting", uiRoot);
            Button uiSettingQuit = comp.Get<Button>("UI_Setting_Quit");
            Listener.Listener.AddListener(uiSettingQuit, QuitGame);
        }

        private void QuitGame() {
            Debug.Log("QuitGame");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}