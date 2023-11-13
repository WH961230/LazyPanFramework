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
            new Go(++Game.Instance.Setting.InstanceID, "Obj_MainTerrain");
            new Go(++Game.Instance.Setting.InstanceID, "Obj_MainDirectionalLight");
            new Go(++Game.Instance.Setting.InstanceID, "Obj_MainVolume");

            DataBody dataBody = new DataBody();
            dataBody.ID = ++Game.Instance.Setting.InstanceID;
            dataBody.Go = new Go(dataBody.ID, "Obj_MainPlayer");
            dataBody.GoInstanceID = dataBody.Go.UGo.GetInstanceID();
            ObjConfig playerConfig = ObjConfig.Get("Obj_MainPlayer");
            dataBody.Type = playerConfig.Type;
            Data.Instance.dataBodyDic.TryAdd(dataBody.ID, dataBody);

            List<Behaviour> behaviours = new List<Behaviour>();
            string[] behaviourArray = playerConfig.Behaviour.Split("|");
            int length = behaviourArray.Length;
            for (int i = 0; i < length; i++) {
                string behaviourSign = behaviourArray[i];
                Behaviour behaviour;
                Type type = Assembly.Load("Assembly-CSharp").GetType(string.Concat("LazyPan.", behaviourSign));
                behaviour = (Behaviour) Activator.CreateInstance(type, dataBody.ID);
                behaviours.Add(behaviour);
            }

            if (Data.Instance.dataBodyDic.TryGetValue(dataBody.ID, out dataBody)) {
                dataBody.Behaviours = behaviours;
                Data.Instance.dataBodyDic[dataBody.ID] = dataBody;
            }
            

            DataBody mainCameraDataBody = new DataBody();
            mainCameraDataBody.ID = ++Game.Instance.Setting.InstanceID;
            mainCameraDataBody.Go = new Go(mainCameraDataBody.ID, "Obj_MainCamera");
            mainCameraDataBody.GoInstanceID = mainCameraDataBody.Go.UGo.GetInstanceID();

            ObjConfig mainCameraConfig = ObjConfig.Get("Obj_MainCamera");
            mainCameraDataBody.Type = mainCameraConfig.Type;
            Data.Instance.dataBodyDic.TryAdd(mainCameraDataBody.ID, mainCameraDataBody);

            List<Behaviour> mainCameraBehaviours = new List<Behaviour>();
            string[] mainCameraBehaviourArray = mainCameraConfig.Behaviour.Split("|");
            length = mainCameraBehaviourArray.Length;
            for (int i = 0; i < length; i++) {
                string behaviourSign = mainCameraBehaviourArray[i];
                Type type = Assembly.Load("Assembly-CSharp").GetType(string.Concat("LazyPan.", behaviourSign));
                Behaviour behaviour = (Behaviour) Activator.CreateInstance(type, mainCameraDataBody.ID);
                mainCameraBehaviours.Add(behaviour);
            }

            if (Data.Instance.dataBodyDic.TryGetValue(mainCameraDataBody.ID, out mainCameraDataBody)) {
                mainCameraDataBody.Behaviours = mainCameraBehaviours;
                Data.Instance.dataBodyDic[mainCameraDataBody.ID] = mainCameraDataBody;
            }
        }
    }
}