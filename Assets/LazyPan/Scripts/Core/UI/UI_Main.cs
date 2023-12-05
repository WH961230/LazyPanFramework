﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class UI_Main : Singleton<UI_Main> {
        private Comp uiMainComp;
        public Comp UIMainComp {
            get {
                if (uiMainComp == null) {
                    uiMainComp = UI.Instance.Get("UI_Main");
                }

                return uiMainComp;
            }
        }

        public void OnInit() {
            UI.Instance.OnAddOwnedDataBody.AddListener(GetEmptyObjGrid);
        }

        public void GetEmptyObjGrid(DataBody dataBody) {
            for (int i = 0; i < UIMainComp.Transforms.Count; i++) {
                Comp.TransformData tranData = UIMainComp.Transforms[i];
                if (tranData.Sign.Contains("UI_Main_Obj_")) {
                    Sprite sprite = tranData.Tran.GetComponent<Image>().sprite;
                    if (sprite == null) {
                        tranData.Tran.GetComponent<Image>().sprite = dataBody.IconSprite;
                        break;
                    }
                }
            }
        }

        public void GetEmptyBehaviourGrid() {
            
        }
    }
}