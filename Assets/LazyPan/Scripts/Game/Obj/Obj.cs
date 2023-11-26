using System.Collections.Generic;
using Mirror;
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
            DataBody dataBody = new DataBody();
            dataBody.ID = Game.Instance.Setting.InstanceID == 0 ? 0 : Game.Instance.Setting.InstanceID;
            --Game.Instance.Setting.InstanceID;
            dataBody.Go = new Go(dataBody.ID, sign);
            dataBody.GoSign = sign;
            dataBody.GoInstanceID = dataBody.Go.UGo.GetInstanceID();
            ObjConfig config = ObjConfig.Get(sign);
            dataBody.Health = config.Health;
            dataBody.Type = config.Type;
            dataBody.Behaviours = new List<Behaviour>();
            Data.Instance.dataBodyDic.TryAdd(dataBody.ID, dataBody);
            AddBehaviourFromConfig(dataBody.ID, sign, dataBody.Go.Comp);
            return dataBody.ID;
        }

        public uint LoadObj(uint netID, bool isLocal, GameObject uGo) {
            DataBody dataBody = new DataBody();
            string objSign = uGo.GetComponent<Comp>().ObjSign;
            dataBody.ID = netID;
            dataBody.Go = new Go(dataBody.ID, objSign, uGo);
            dataBody.GoSign = objSign;
            dataBody.isLocalMainPlayer = isLocal;
            dataBody.GoInstanceID = dataBody.Go.UGo.GetInstanceID();
            ObjConfig config = ObjConfig.Get(objSign);
            dataBody.Health = config.Health;
            dataBody.Type = config.Type;
            dataBody.Behaviours = new List<Behaviour>();
            Data.Instance.dataBodyDic.TryAdd(dataBody.ID, dataBody);
            AddBehaviourFromConfig(dataBody.ID, objSign, dataBody.Go.Comp);
            return dataBody.ID;
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