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
                        break;
                    }
                }
            }
        }

        public void GetObjGridByIndex() {
            
        }
    }
}