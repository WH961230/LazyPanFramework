﻿using UnityEditor;
using UnityEngine;

namespace LazyPan {
    public class Go {
        public uint ID;
        public GameObject UGo;
        public Comp Comp;

        public Go(uint id, string sign) {
            ID = id;
            GoType goType = (GoType) ObjConfig.Get(sign).Type;
            UGo = Loader.LoadGo("", string.Concat("Obj/", sign), GetRoot(goType), true);
            UGo.name = string.Concat(GetGoName(goType), "_", UGo.GetInstanceID(), "_", ID);
            Comp = UGo.GetComponent<Comp>();
#if UNITY_EDITOR
            if (goType == GoType.Player) {
                Texture2D icon = EditorGUIUtility.IconContent("sv_label_0").image as Texture2D;
                EditorGUIUtility.SetIconForObject(UGo, icon);
            }
#endif
        }

        public Go(uint id, string sign, GameObject uGo) {
            ID = id;
            GoType goType = (GoType) ObjConfig.Get(sign).Type;
            UGo = uGo;
            UGo.transform.parent = GetRoot(goType);
            UGo.name = string.Concat(GetGoName(goType), "_", UGo.GetInstanceID(), "_", ID);
            Comp = UGo.GetComponent<Comp>();
#if UNITY_EDITOR
            if (goType == GoType.Player) {
                Texture2D icon = EditorGUIUtility.IconContent("sv_label_0").image as Texture2D;
                EditorGUIUtility.SetIconForObject(UGo, icon);
            }
#endif
        }

        private string GetGoName(GoType type) {
            string path = null;
            switch (type) {
                case GoType.Player:
                    path = "玩家";
                    break;
                case GoType.Camera:
                    path = "相机";
                    break;
                case GoType.Light:
                    path = "灯光";
                    break;
                case GoType.Terrain:
                    path = "地形";
                    break;
                case GoType.Volume:
                    path = "后处理";
                    break;
                case GoType.PickableObj:
                    path = "可拾取物体";
                    break;
                case GoType.PickableBehaviourObj:
                    path = "可拾取行为物体";
                    break;
            }

            return path;
        }

        private Transform GetRoot(GoType type) {
            Transform root = null;
            switch (type) {
                case GoType.PickableBehaviourObj:
                case GoType.PickableObj:
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
        PickableObj,
        PickableBehaviourObj,
    }
}