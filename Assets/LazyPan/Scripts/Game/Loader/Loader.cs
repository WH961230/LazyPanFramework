using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace LazyPan {
    public partial class Loader {
        private static string INPUTACTIONASSET_PATH = "Assets/LazyPan/Bundles/Configs/";
        private static string INPUTACTIONASSET_SUFFIX = ".inputactions";

        private static string PREFAB_PATH = "Assets/LazyPan/Bundles/Prefabs/";
        private static string PREFAB_SUFFIX = ".prefab";

        public static T Load<T>(LoaderType type, string assetPath) where T : Object {
            string path = "";
            switch (type) {
                case LoaderType.PREFAB:
                    path = String.Concat(String.Concat(PREFAB_PATH, assetPath), PREFAB_SUFFIX);
                    break;
                case LoaderType.INPUTACTIONASSET:
                    path = String.Concat(String.Concat(INPUTACTIONASSET_PATH, assetPath), INPUTACTIONASSET_SUFFIX);
                    break;
            }
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static GameObject Load(string name, string path, Transform parent) {
            GameObject prefab = Load<GameObject>(LoaderType.PREFAB, path);
            GameObject go = Object.Instantiate(prefab, parent);
            go.name = name;
            return go;
        }

        public static GameObject[] Load(string folderPath, string filter, Transform parent, bool isActive) {
            string[] prefabGuids = AssetDatabase.FindAssets(filter, new[] { PREFAB_PATH + folderPath });
            GameObject[] rets = new GameObject[prefabGuids.Length];
            for (int i = 0 ; i < prefabGuids.Length; i++) {
                string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuids[i]);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                GameObject go = Object.Instantiate(prefab, parent);
                go.name = prefab.name;
                go.SetActive(isActive);
                rets[i] = go;
            }
            return rets;
        }

        public static GameComp LoadComp(string name, string path, Transform parent) {
            GameObject go = Load(name, path, parent);
            return go.GetComponent<GameComp>();
        }

        public static AsyncOperation LoadSceneAsync(string name) {
            return SceneManager.LoadSceneAsync(name);
        }

        [Serializable]
        public enum LoaderType {
            PREFAB,
            INPUTACTIONASSET,
        }
    }
}