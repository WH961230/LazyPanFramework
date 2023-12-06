﻿using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class UI_Main : Singleton<UI_Main> {
        private GameObject displayedObj;
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
            Act.SwitchObj(Vector2.zero);
            UI.Instance.OnAddOwnedEntity.AddListener(GetEmptyObjGrid);
        }

        public void GetEmptyObjGrid(Entity entity) {
            for (int i = 0; i < UIMainComp.Transforms.Count; i++) {
                Comp.TransformData tranData = UIMainComp.Transforms[i];
                if (tranData.Sign.Contains("UI_Main_Obj_Icon_")) {
                    Image image = tranData.Tran.GetComponent<Image>();
                    Sprite sprite = image.sprite;
                    if (image.enabled == false && sprite == null) {
                        tranData.Tran.GetComponent<Image>().sprite = entity.IconSprite;
                        image.enabled = true;
                        tranData.Tran.name = string.Concat("UI_Main_Obj_Icon_", i , "_", entity.ID);
                        break;
                    }
                }
            }
        }

        public void DisplaySelectObj() {
            Transform select = UIMainComp.Get<Transform>("UI_Main_ObjSelect");
            Transform selectParent = select.parent;

            string[] signStr = selectParent.name.Split("_");
            if (signStr.Length > 5) {
                uint ID = uint.Parse(signStr[5]);
                if (Data.Instance.TryGetEntityByID(ID, out Entity entity)) {
                    if (displayedObj != null) {
                        Debug.Log("displayed Obj:" + displayedObj.name);
                        displayedObj.SetActive(false);
                    }
                    Debug.Log("Display Obj:" + ID);
                    entity.Go.UGo.SetActive(true);
                    entity.Go.UGo.transform.localPosition = Vector3.zero;
                    displayedObj = entity.Go.UGo;
                }
            }
        }
    }
}