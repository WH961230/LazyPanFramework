using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class UI {
        public Transform UIRoot;
        private Comp UIComp;
        private Dictionary<string, Comp> uICompDics = new Dictionary<string, Comp>();

        public static UI Instance;
        public UI() {
            Instance = this;
            UIPreLoad();
            UIEventRegister();
        }

        private void UIPreLoad() {
            UIRoot = Loader.Load("画布", "Global/Global_UI_Root", null).transform;
            GameObject[] uiGos = Loader.Load("UI", "t:prefab", UIRoot, false);

            uICompDics.Clear();
            for (int i = 0; i < uiGos.Length; i++) {
                uICompDics.Add(uiGos[i].name, uiGos[i].GetComponent<Comp>());
            }

            Debug.Log(Config.Instance.Get<string>("PlayerConfig", "Lisi"));
            Debug.Log(Config.Instance.Get<string>("UIConfig", "UI_Setting"));
        }

        private void UIEventRegister() {
            Listener.AddListener(Get("UI_Setting").Get<Button>("UI_Setting_Quit"), Act.QuitGame);
            Listener.AddListener(Get("UI_Setting").Get<Button>("UI_Setting_Close"), Close, "UI_Setting");
        }

        public void Load() {
            
        }

        public Comp Open(string name) {
            if (UIComp != null) {
                UIComp.gameObject.SetActive(false);
            }

            if (uICompDics.TryGetValue(name, out Comp comp)) {
                UIComp = comp;
                UIComp.gameObject.SetActive(true);
                return UIComp;
            }

            return null;
        }

        public Comp Get(string name) {
            if (uICompDics.TryGetValue(name, out Comp comp)) {
                return comp;
            }

            return null;
        }

        public bool IsUI(string name) {
            if (UIComp == null) {
                return false;
            }

            return UIComp.gameObject.name == name;
        }

        public void Close() {
            Close(UIComp);
            UIComp = null;
        }

        public void Close(string name) {
            if (IsUI(name)) {
                Close(Get(name));
                UIComp = null;
            }
        }

        private void Close(Comp comp) {
            comp.gameObject.SetActive(false);
        }
    }
}