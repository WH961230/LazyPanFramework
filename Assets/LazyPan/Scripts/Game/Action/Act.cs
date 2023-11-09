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
    }
}