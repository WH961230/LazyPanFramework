using UnityEngine;

namespace LazyPan {
    public class Behaviour {
        protected uint SubjectID;
        protected string BehaviouorSign;
        protected Go SubjectGo;
        protected GameObject SubjectUGo;
        protected Comp SubjectComp;
        protected bool SubjectIsLocal;
        protected Sprite SubjectIcon;
        protected BehaviourData SubjectBehaviourData;

        protected Behaviour(uint subjectId, string behaviourSign) {
            SubjectID = subjectId;
            BehaviouorSign = behaviourSign;
            if (SubjectID != -1) {
                SubjectGo = Data.Instance.EntityDic[SubjectID].Go;
                SubjectUGo = SubjectGo.UGo;
                SubjectComp = SubjectGo.Comp;
                SubjectIcon = Loader.LoadAsset<Sprite>(Loader.AssetType.SPRITE, behaviourSign);
                SubjectBehaviourData = Data.Instance.EntityDic[SubjectID].BehaviourData;
                SubjectIsLocal = Data.Instance.EntityDic[SubjectID].isLocalMainPlayer;
            }
        }

        public Sprite GetBehaviourIcon() {
            return Loader.LoadAsset<Sprite>(Loader.AssetType.SPRITE, BehaviouorSign);
        }

        public string GetBehaviourSign() {
            return BehaviouorSign;
        }

        protected bool IsSelected() {
            return UI_Main.Instance.GetSelectedBehaviourSign() == BehaviouorSign;
        }
    }

    public class BehaviourData {
        public float GravityValue;
        public bool IsGrounded;
        public bool IsJumping;
        public bool IsFlying;
        public Vector3 MoveVec;
    }
}