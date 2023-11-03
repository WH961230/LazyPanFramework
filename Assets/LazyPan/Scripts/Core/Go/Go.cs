using UnityEditor;
using UnityEngine;

namespace LazyPan {
    public class Go {
        public int ID;
        public GameObject UGo;

        public Go(GoType type, string name) {
            ID = ++Game.Instance.Setting.InstanceID;
            UGo = Loader.Load("", GetPath(type), GetRoot(type));
            int uGoID = UGo.GetInstanceID();
            UGo.name = string.Concat(name, "_", uGoID, "_", ID);
            if (type == GoType.Player) {
                Texture2D icon = EditorGUIUtility.IconContent("sv_label_0").image as Texture2D;
                EditorGUIUtility.SetIconForObject(UGo, icon);
            }
            Data.Instance.goID.TryAdd(uGoID, ID);
            Data.Instance.go.TryAdd(ID, this);
        }

        private string GetPath(GoType type) {
            string path = null;
            switch (type) {
                case GoType.Player:
                    path = "Obj/Obj_Player";
                    break;
                case GoType.Camera:
                    path = "Camera/Camera_Main";
                    break;
                case GoType.Light:
                    path = "Light/Light_Directional";
                    break;
                case GoType.Terrain:
                    path = "Terrain/Terrain_Main";
                    break;
                case GoType.Volume:
                    path = "Volume/Volume";
                    break;
            }
            return path;
        }

        private Transform GetRoot(GoType type) {
            Transform root = null;
            switch (type) {
                case GoType.Player:
                    root = Obj.Instance.ObjRoot;
                    break;
                case GoType.Camera:
                    root = Obj.Instance.CameraRoot;
                    break;
                case GoType.Light:
                    root = Obj.Instance.LightRoot;
                    break;
                case GoType.Terrain:
                    root = Obj.Instance.TerrainRoot;
                    break;
                case GoType.Volume:
                    root = Obj.Instance.VolumeRoot;
                    break;
            }
            return root;
        }
    }

    public enum GoType {
        Player,
        Camera,
        Light,
        Terrain,
        Volume,
    }
}