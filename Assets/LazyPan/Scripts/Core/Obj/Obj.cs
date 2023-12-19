using System.Collections.Generic;
using UnityEngine;

namespace LazyPan {
    public class Obj : Singleton<Obj> {
        public Transform ObjRoot;
        public Transform CameraRoot;
        public Transform LightRoot;
        public Transform VolumeRoot;
        public Transform TerrainRoot;

        public void Init() {
            ObjRoot = Loader.LoadGo("物体", "Global/Global_Obj_Root", null, true).transform;
            CameraRoot = Loader.LoadGo("相机", "Global/Global_Camera_Root", null, true).transform;
            LightRoot = Loader.LoadGo("灯光", "Global/Global_Light_Root", null, true).transform;
            VolumeRoot = Loader.LoadGo("后处理", "Global/Global_Volume_Root", null, true).transform;
            TerrainRoot = Loader.LoadGo("地形", "Global/Global_Terrain_Root", null, true).transform;
        }

        public uint LoadSignObj(string sign) {
            Entity entity = new Entity();
            entity.ID = Game.Instance.Setting.InstanceID == 0 ? 0 : Game.Instance.Setting.InstanceID;
            --Game.Instance.Setting.InstanceID;
            entity.Go = new Go(entity.ID, sign);
            entity.GoSign = sign;
            entity.GoInstanceID = entity.Go.UGo.GetInstanceID();
            ObjConfig config = ObjConfig.Get(sign);
            entity.Health = config.Health;
            entity.Type = config.Type;
            entity.Behaviours = new List<Behaviour>();
            Data.Instance.EntityDic.TryAdd(entity.ID, entity);
            AddBehaviourFromConfig(entity.ID, sign, entity.Go.Comp);
            return entity.ID;
        }

        public uint LoadObj(uint netID, bool isLocal, GameObject uGo) {
            Entity entity = new Entity();
            string objSign = uGo.GetComponent<Comp>().ObjSign;
            entity.ID = netID;
            entity.Go = new Go(entity.ID, objSign, uGo);
            entity.GoSign = objSign;
            entity.isLocalMainPlayer = isLocal;
            entity.GoInstanceID = entity.Go.UGo.GetInstanceID();
            ObjConfig config = ObjConfig.Get(objSign);
            entity.IconSprite = Loader.LoadAsset<Sprite>(Loader.AssetType.SPRITE, objSign);
            entity.Health = config.Health;
            entity.Type = config.Type;
            entity.Behaviours = new List<Behaviour>();
            Data.Instance.EntityDic.TryAdd(entity.ID, entity);
            AddBehaviourFromConfig(entity.ID, objSign, entity.Go.Comp);
            return entity.ID;
        }

        public void AddBehaviourFromConfig(uint id, string objSign, Comp comp) {
            ObjConfig config = ObjConfig.Get(objSign);
            if (!string.IsNullOrEmpty(config.Behaviour)) {
                string[] behaviourArray = config.Behaviour.Split("|");
                int length = behaviourArray.Length;
                for (int i = 0; i < length; i++) {
                    string behaviourSign = behaviourArray[i];
                    comp.BehaviourBundles.Add(behaviourSign);
                    Data.Instance.AddBehaviour(id, behaviourSign);
                }
            }
        }
    }
}