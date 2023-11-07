using UnityEditor;
using UnityEngine;

namespace LazyPan {
    public class Go {
        public int ID;
        public GameObject UGo;

        public Go(GoType type, string sign, string name) {
            ID = ++Game.Instance.Setting.InstanceID;
            UGo = Loader.Load("", GetPath(type, sign), GetRoot(type));
            int uGoID = UGo.GetInstanceID();
            UGo.name = string.Concat(name, "_", uGoID, "_", ID);

            Data.Instance.goID.TryAdd(uGoID, ID);
            Data.Instance.go.TryAdd(ID, this);
            Debug.LogFormat("Go ID : {0} , UGo ID : {1} , Name : {2}", ID, uGoID, UGo.name);

            GizmosIcon(type);
        }

        private void GizmosIcon(GoType type) {
            if (type == GoType.Player) {
                Texture2D icon = EditorGUIUtility.IconContent("sv_label_0").image as Texture2D;
                EditorGUIUtility.SetIconForObject(UGo, icon);
            }
        }

        private string GetPath(GoType type, string sign) {
            string path = null;
            switch (type) {
                case GoType.Obj:
                case GoType.Player:
                    path = $"Obj/Obj_{sign}";
                    break;
                case GoType.Camera:
                    path = $"Camera/Camera_{sign}";
                    break;
                case GoType.Light:
                    path = $"Light/Light_{sign}";
                    break;
                case GoType.Terrain:
                    path = $"Terrain/Terrain_{sign}";
                    break;
                case GoType.Volume:
                    path = $"Volume/Volume";
                    break;
            }
            return path;
        }

        private Transform GetRoot(GoType type) {
            Transform root = null;
            switch (type) {
                case GoType.Obj:
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
        Obj,
        Player,
        Camera,
        Light,
        Terrain,
        Volume,
    }
}