using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LazyPan {
    public class Comp : MonoBehaviour {
        public Action<Collider> OnTriggerEnterAction;

        public List<GameObjectData> GameObjects = new List<GameObjectData>();
        public List<ButtonData> Buttons = new List<ButtonData>();
        public List<SliderData> Sliders = new List<SliderData>();
        public List<TextData> Texts = new List<TextData>();
        public List<TextMeshProUGUIData> TextMeshProUGUIs = new List<TextMeshProUGUIData>();

        public T Get<T>(string sign) where T : Object {
            if (typeof(T) == typeof(GameObject)) {
                foreach (GameObjectData gameObjectData in GameObjects) {
                    if (gameObjectData.Sign == sign) {
                        return gameObjectData.GO as T;
                    }
                }
            } else if (typeof(T) == typeof(Button)) {
                foreach (ButtonData buttonData in Buttons) {
                    if (buttonData.Sign == sign) {
                        return buttonData.Button as T;
                    }
                }
            } else if (typeof(T) == typeof(Slider)) {
                foreach (SliderData sliderData in Sliders) {
                    if (sliderData.Sign == sign) {
                        return sliderData.Slider as T;
                    }
                }
            } else if (typeof(T) == typeof(Text)) {
                foreach (TextData textData in Texts) {
                    if (textData.Sign == sign) {
                        return textData.Text as T;
                    }
                }
            } else if (typeof(T) == typeof(TextMeshProUGUI)) {
                foreach (TextMeshProUGUIData textMeshProUGUIData in TextMeshProUGUIs) {
                    if (textMeshProUGUIData.Sign == sign) {
                        return textMeshProUGUIData.TextMeshProUGUI as T;
                    }
                }
            }

            return null;
        }

        public void OnTriggerEnter(Collider other) {
            OnTriggerEnterAction?.Invoke(other);
        }

        [Serializable]
        public class ButtonData {
            public string Sign;
            public Button Button;
        }

        [Serializable]
        public class SliderData {
            public string Sign;
            public Slider Slider;
        }

        [Serializable]
        public class TextData {
            public string Sign;
            public Text Text;
        }

        [Serializable]
        public class TextMeshProUGUIData {
            public string Sign;
            public TextMeshProUGUI TextMeshProUGUI;
        }

        [Serializable]
        public class GameObjectData {
            public string Sign;
            public GameObject GO;
        }
    }
}