using System;
using LazyPan.Scripts.Core.Component;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace LazyPan.Scripts.Game.Loader {
    public static class Loader {
        private static string PREFAB_PATH = "Assets/LazyPan/Bundles/Prefabs/";
        private static string PREFAB_SUFFIX = ".prefab";

        public static GameObject Load(string name, string path, Transform parent) {
            path = String.Concat(String.Concat(PREFAB_PATH, path), PREFAB_SUFFIX);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObject go = Object.Instantiate(prefab, parent);
            go.name = name;
            return go;
        }

        public static GameComp LoadComp(string name, string path, Transform parent) {
            GameObject go = Load(name, path, parent);
            return go.GetComponent<GameComp>();
        }

        public static void LoadScene(string name) {
            SceneManager.LoadScene(name);
        }

        public static AsyncOperation LoadSceneAsync(string name) {
            return SceneManager.LoadSceneAsync(name);
        }
    }
}