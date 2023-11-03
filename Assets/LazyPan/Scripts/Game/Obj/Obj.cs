using UnityEngine;

namespace LazyPan {
    public class Obj {
        public Transform ObjRoot;
        public Transform CameraRoot;
        public Transform LightRoot;
        public Transform VolumeRoot;
        public Transform TerrainRoot;

        public static Obj Instance;
        public Obj() {
            Instance = this;
            ObjRoot = Loader.Load("物体", "Global/Global_Obj_Root", null).transform;
            CameraRoot = Loader.Load("相机", "Global/Global_Camera_Root", null).transform;
            LightRoot = Loader.Load("灯光", "Global/Global_Light_Root", null).transform;
            VolumeRoot = Loader.Load("后处理", "Global/Global_Volume_Root", null).transform;
            TerrainRoot = Loader.Load("地形", "Global/Global_Terrain_Root", null).transform;
        }

        public void Load() {
            new Go(GoType.Terrain, "地形");
            new Go(GoType.Camera, "主相机");
            new Go(GoType.Light, "直射灯光");
            new Go(GoType.Volume, "后处理");

            Go go1 = new Go(GoType.Player, "玩家");
            new Function(go1.ID, new Behaviour_Move(go1.ID));
            Go go2 = new Go(GoType.Player, "玩家");
            Go go3 = new Go(GoType.Player, "玩家");
        }
    }
}