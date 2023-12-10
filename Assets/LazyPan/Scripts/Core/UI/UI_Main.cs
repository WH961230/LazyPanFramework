using UnityEngine;
using UnityEngine.UI;

namespace LazyPan {
    public class UI_Main : Singleton<UI_Main> {
        private Image displayBehaviourImage;
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
            UI.Instance.OnAddBehaviour.AddListener(GetEmptyBehaviourGrid);
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

        public void GetEmptyBehaviourGrid(Behaviour behaviour) {
            for (int i = 0; i < UIMainComp.Transforms.Count; i++) {
                Comp.TransformData tranData = UIMainComp.Transforms[i];
                if (tranData.Sign.Contains("UI_Main_Behaviour_Icon_")) {
                    Image image = tranData.Tran.GetComponent<Image>();
                    Sprite sprite = image.sprite;
                    if (image.enabled == false && sprite == null) {
                        tranData.Tran.GetComponent<Image>().sprite = behaviour.GetBehaviourIcon();
                        image.enabled = true;
                        tranData.Tran.name = string.Concat("UI_Main_Behaviour_Icon_", i , "_", behaviour.GetBehaviourSign());
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
                        displayedObj.SetActive(false);
                    }
                    entity.Go.UGo.SetActive(true);
                    entity.Go.UGo.transform.localPosition = Vector3.zero;
                    displayedObj = entity.Go.UGo;
                }
            } else {
                if (displayedObj != null) {
                    displayedObj.SetActive(false);
                }
            }
        }

        public void DisplaySelectBehaviour() {
        }

        public string GetSelectedBehaviourSign() {
            Transform select = UIMainComp.Get<Transform>("UI_Main_BehaviourSelect");
            Transform selectParent = select.parent;

            string behaviourSign = "";
            string[] signStr = selectParent.name.Split("_");
            if (signStr.Length > 6) {
                behaviourSign = string.Concat(signStr[5], "_",signStr[6]);
            }

            return behaviourSign;
        }

        public GameObject GetSelectedObj() {
            return displayedObj;
        }
    }
}