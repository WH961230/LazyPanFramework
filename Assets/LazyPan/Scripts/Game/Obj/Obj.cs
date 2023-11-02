using UnityEngine;

namespace LazyPan {
    public class Obj {
        public Transform ObjRoot;
        public Transform CameraRoot;
        public Transform LightRoot;
        public Transform VolumeRoot;
        public Transform TerrainRoot;
        public static Obj Instance;

        public void Start() {
            Instance = this;
            ObjRoot = Loader.Load("物体", "Global/Global_Obj_Root", null).transform;
            CameraRoot = Loader.Load("相机", "Global/Global_Camera_Root", null).transform;
            LightRoot = Loader.Load("灯光", "Global/Global_Light_Root", null).transform;
            VolumeRoot = Loader.Load("后处理", "Global/Global_Volume_Root", null).transform;
            TerrainRoot = Loader.Load("地形", "Global/Global_Terrain_Root", null).transform;
        }

        public void Load() {
            Loader.Load("地形", "Terrain/Terrain_Main", TerrainRoot);
            Loader.Load("主相机", "Camera/Camera_Main", CameraRoot);
            Loader.Load("后处理", "Volume/Volume", VolumeRoot);
            Loader.Load("直射灯光", "Light/Light_Directional", LightRoot);
            Loader.Load("玩家", "Obj/Obj_Player", ObjRoot);
        }
    }
}