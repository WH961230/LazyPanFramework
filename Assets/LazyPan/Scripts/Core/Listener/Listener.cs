using UnityEngine.Events;
using UnityEngine.UI;

namespace LazyPan {
    public static class Listener {
        public static void AddListener(Button button, UnityAction unityAction) {
            button.onClick.AddListener(unityAction);
        }

        public static void AddListener<T>(Button button, UnityAction<T> unityAction, T t) {
            button.onClick.AddListener(() => unityAction(t));
        }

        public static void RemoveListener(Button button, UnityAction unityAction) {
            button.onClick.RemoveListener(unityAction);
        }

        public static void RemoveListener<T>(Button button, UnityAction<T> unityAction, T t) {
            button.onClick.RemoveListener(() => unityAction(t));
        }

        public static void RemoveAllListener(Button button) {
            button.onClick.RemoveAllListeners();
        }
    }
}