using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class UI {
        public Transform UIRoot;
        private Comp UIComp;
        private Dictionary<string, Comp> uICompAlwaysDics = new Dictionary<string, Comp>();
        private Dictionary<string, Comp> uICompExchangeDics = new Dictionary<string, Comp>();
        private Dictionary<string, Comp> uICompDics = new Dictionary<string, Comp>();

        public static UI Instance;
        public UI() {
            Instance = this;
            UIPreLoad();
            UIEventRegister();
        }

        private void UIPreLoad() {
            UIRoot = Loader.LoadGo("画布", "Global/Global_UI_Root", null, true).transform;

            List<string> keys = UIConfig.GetKeys();
            int length = keys.Count;

            uICompDics.Clear();
            uICompExchangeDics.Clear();
            uICompAlwaysDics.Clear();

            for (int i = 0; i < length; i++) {
                string key = keys[i];
                GameObject uiGo = Loader.LoadGo(key, string.Concat("UI/",key), UIRoot, false);
                switch (UIConfig.Get(key).Type) {
                    case 0:
                        uICompExchangeDics.Add(key, uiGo.GetComponent<Comp>());
                        break;
                    case 1:
                        uICompAlwaysDics.Add(key, uiGo.GetComponent<Comp>());
                        break;
                }

                uICompDics.Add(key, uiGo.GetComponent<Comp>());
            }
        }

        private void UIEventRegister() {
            Listener.AddListener(Get("UI_Setting").Get<Button>("UI_Setting_Quit"), Act.QuitGame);
            Listener.AddListener(Get("UI_Setting").Get<Button>("UI_Setting_Close"), Close, "UI_Setting");
        }

        public void Load() {
            
        }

        public Comp Open(string name) {
            if (uICompExchangeDics.TryGetValue(name, out Comp uiExchangeComp)) {
                if (UIComp != null) {
                    UIComp.gameObject.SetActive(false);
                }

                UIComp = uiExchangeComp;
                UIComp.gameObject.SetActive(true);
                return UIComp;
            }

            if (uICompAlwaysDics.TryGetValue(name, out Comp uiAlwaysComp)) {
                uiAlwaysComp.gameObject.SetActive(true);
                return uiAlwaysComp;
            }

            return null;
        }

        public Comp Get(string name) {
            if (uICompDics.TryGetValue(name, out Comp comp)) {
                return comp;
            }

            return null;
        }

        public string GetExchangeUIName() {
            if (UIComp != null) {
                return UIComp.gameObject.name;
            }

            return null;
        }

        public bool IsExchangeUI() {
            return UIComp != null;
        }

        public void Close() {
            Close(UIComp);
            UIComp = null;
        }

        public void Close(string name) {
            if (uICompAlwaysDics.TryGetValue(name, out Comp uiAlwaysComp)) {
                Close(uiAlwaysComp);
            }
        }

        private void Close(Comp comp) {
            if (comp != null) {
                comp.gameObject.SetActive(false);
            }
        }
    }
}