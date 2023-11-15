﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LazyPan {
    public class Comp : MonoBehaviour {
        [HideInInspector] public UnityEvent<Collider> OnTriggerEnterEvent;
        [HideInInspector] public UnityEvent<Collider> OnTriggerStayEvent;
        [HideInInspector] public UnityEvent<Collider> OnTriggerExitEvent;
        [HideInInspector] public UnityEvent OnDrawGizmosAction;

        public List<GameObjectData> GameObjects = new List<GameObjectData>();
        public List<TransformData> Transforms = new List<TransformData>();
        public List<CharacterControllerData> CharacterControllers = new List<CharacterControllerData>();
        public List<ButtonData> Buttons = new List<ButtonData>();
        public List<SliderData> Sliders = new List<SliderData>();
        public List<TextData> Texts = new List<TextData>();
        public List<TextMeshProUGUIData> TextMeshProUGUIs = new List<TextMeshProUGUIData>();

        public T Get<T>(string sign) where T : Object {
            if (typeof(T) == typeof(CharacterController)) {
                foreach (CharacterControllerData controllerData in CharacterControllers) {
                    if (controllerData.Sign == sign) {
                        return controllerData.Controller as T;
                    }
                }
            } else if (typeof(T) == typeof(GameObject)) {
                foreach (GameObjectData gameObjectData in GameObjects) {
                    if (gameObjectData.Sign == sign) {
                        return gameObjectData.GO as T;
                    }
                }
            } else if (typeof(T) == typeof(Transform)) {
                foreach (TransformData transformData in Transforms) {
                    if (transformData.Sign == sign) {
                        return transformData.Tran as T;
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

        public void OnTriggerEnter(Collider other) { OnTriggerEnterEvent?.Invoke(other); }
        public void OnTriggerStay(Collider other) { OnTriggerStayEvent?.Invoke(other); }
        private void OnTriggerExit(Collider other) { OnTriggerExitEvent?.Invoke(other); }

#if UNITY_EDITOR
        private void OnDrawGizmos() { OnDrawGizmosAction.Invoke(); }
#endif

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

        [Serializable]
        public class TransformData {
            public string Sign;
            public Transform Tran;
        }

        [Serializable]
        public class CharacterControllerData {
            public string Sign;
            public CharacterController Controller;
        }
    }
}