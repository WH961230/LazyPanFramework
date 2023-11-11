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

        public void Load() {
            new Go(++Game.Instance.Setting.InstanceID, "Obj_MainTerrain");
            new Go(++Game.Instance.Setting.InstanceID, "Obj_MainDirectionalLight");
            new Go(++Game.Instance.Setting.InstanceID, "Obj_MainVolume");

            DataBody dataBody = new DataBody();
            dataBody.ID = ++Game.Instance.Setting.InstanceID;
            dataBody.Go = new Go(dataBody.ID, "Obj_Player");
            dataBody.GoInstanceID = dataBody.Go.UGo.GetInstanceID();

            var behaviours = new List<Behaviour>();
            behaviours.Add(new Behaviour_Move(dataBody.ID, -1));
            dataBody.Behaviours = behaviours;
            Data.Instance.dataBodyDic.TryAdd(dataBody.ID, dataBody);

            DataBody mainCameraDataBody = new DataBody();
            mainCameraDataBody.ID = ++Game.Instance.Setting.InstanceID;
            mainCameraDataBody.Go = new Go(mainCameraDataBody.ID, "Obj_MainCamera");
            mainCameraDataBody.GoInstanceID = mainCameraDataBody.Go.UGo.GetInstanceID();

            var mainCameraBehaviours = new List<Behaviour>();
            mainCameraBehaviours.Add(new Behaivour_Follow(mainCameraDataBody.ID, dataBody.ID));
            mainCameraBehaviours.Add(new Behaviour_Look(mainCameraDataBody.ID, dataBody.ID));
            mainCameraBehaviours.Add(new Behaviour_InputView(dataBody.ID, -1));
            mainCameraDataBody.Behaviours = mainCameraBehaviours;
            Data.Instance.dataBodyDic.TryAdd(mainCameraDataBody.ID, mainCameraDataBody);
        }
    }
}