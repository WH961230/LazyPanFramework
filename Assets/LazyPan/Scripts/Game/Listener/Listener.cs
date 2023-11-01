using UnityEngine.Events;
using UnityEngine.UI;

namespace LazyPan.Scripts.Game.Listener {
    public static class Listener {
        public static void AddListener(Button button, UnityAction unityAction) {
            button.onClick.AddListener(unityAction);
        }

        public static void RemoveListener(Button button, UnityAction unityAction) {
            button.onClick.RemoveListener(unityAction);
        }

        public static void RemoveAllListener(Button button) {
            button.onClick.RemoveAllListeners();
        }
    }
}