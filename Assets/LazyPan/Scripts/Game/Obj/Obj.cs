using System;
using System.Collections.Generic;
using System.Reflection;
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

        public void Load() {
            LoadObj("Obj_MainTerrain");
            LoadObj("Obj_MainDirectionalLight");
            LoadObj("Obj_MainVolume");
            LoadObj("Obj_OtherPlayer");
            LoadObj("Obj_MainCamera");
            LoadObj("Obj_PickableObj_Shoot");
            LoadObj("Obj_PickableObj_Fly");
            LoadObj("Obj_PickableObj_Jump");
        }

        public int LoadObj(string sign) {
            DataBody dataBody = new DataBody();
            dataBody.ID = ++Game.Instance.Setting.InstanceID;
            dataBody.Go = new Go(dataBody.ID, sign);
            dataBody.GoSign = sign;
            dataBody.GoInstanceID = dataBody.Go.UGo.GetInstanceID();
            ObjConfig config = ObjConfig.Get(sign);
            dataBody.Type = config.Type;
            dataBody.Behaviours = new List<Behaviour>();
            Data.Instance.dataBodyDic.TryAdd(dataBody.ID, dataBody);
            AddBehaviourFromConfig(dataBody.ID, sign);
            return dataBody.ID;
        }

        private void AddBehaviourFromConfig(int id, string objSign) {
            ObjConfig config = ObjConfig.Get(objSign);
            if (!string.IsNullOrEmpty(config.Behaviour)) {
                string[] behaviourArray = config.Behaviour.Split("|");
                int length = behaviourArray.Length;
                for (int i = 0; i < length; i++) {
                    string behaviourSign = behaviourArray[i];
                    Data.Instance.AddBehaviour(id, behaviourSign);
                }
            }
        }
    }
}