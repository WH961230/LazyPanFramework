using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Act {
        public static void QuitGame() {
            Debug.Log("QuitGame");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public static void OpenSetting(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                if(UI.Instance.IsUI("UI_Setting")) {
                    UI.Instance.Close();
                } else {
                    UI.Instance.Open("UI_Setting");
                }
            }
        }
    }
}