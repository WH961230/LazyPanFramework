using UnityEngine;
using UnityEngine.AddressableAssets;

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
            ObjRoot = Loader.LoadGo("物体", "Global/Global_Obj_Root", null, true).transform;
            CameraRoot = Loader.LoadGo("相机", "Global/Global_Camera_Root", null, true).transform;
            LightRoot = Loader.LoadGo("灯光", "Global/Global_Light_Root", null, true).transform;
            VolumeRoot = Loader.LoadGo("后处理", "Global/Global_Volume_Root", null, true).transform;
            TerrainRoot = Loader.LoadGo("地形", "Global/Global_Terrain_Root", null, true).transform;
        }

        public void Load() {
            new Go(GoType.Terrain, "Main", "地形");
            new Go(GoType.Camera, "Main", "主相机");
            new Go(GoType.Light, "Directional" , "直射灯光");
            new Go(GoType.Volume, "","后处理");

            Go go1 = new Go(GoType.Player, "Player", "玩家");
            new Function(go1.ID, new Behaviour_Move(go1.ID));

            // Go go2 = new Go(GoType.Player, "玩家");
            // Go go3 = new Go(GoType.Player, "玩家");
        }
    }
}