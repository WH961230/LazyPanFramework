using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace LazyPan {
    public partial class Loader {
        private static string INPUTACTIONASSET_PATH = "Assets/LazyPan/Bundles/Configs/";
        private static string INPUTACTIONASSET_SUFFIX = ".inputactions";

        private static string PREFAB_PATH = "Assets/LazyPan/Bundles/Prefabs/";
        private static string PREFAB_SUFFIX = ".prefab";

        private static string ASSET_PATH = "Assets/LazyPan/Bundles/Configs/Setting/";
        private static string ASSET_SUFFIX = ".asset";

        private static string SOUND_PATH = "Assets/LazyPan/Bundles/Sounds/";
        private static string SOUND_SUFFIX = ".wav";

        private static string GetAddress(AssetType type, string assetName) {
            string address = "";
            switch (type) {
                case AssetType.PREFAB:
                    address = String.Concat(String.Concat(PREFAB_PATH, assetName), PREFAB_SUFFIX);
                    break;
                case AssetType.ASSET:
                    address = String.Concat(String.Concat(ASSET_PATH, assetName), ASSET_SUFFIX);
                    break;
                case AssetType.INPUTACTIONASSET:
                    address = String.Concat(String.Concat(INPUTACTIONASSET_PATH, assetName), INPUTACTIONASSET_SUFFIX);
                    break;
                case AssetType.SOUND:
                    address = String.Concat(String.Concat(SOUND_PATH, assetName), SOUND_SUFFIX);
                    break;
            }

            return address;
        }

        public static T LoadAsset<T>(AssetType type, string assetName) {
            return Addressables.LoadAssetAsync<T>(GetAddress(type, assetName)).WaitForCompletion();
        }

        public static GameObject LoadGo(string finalName, string assetName, Transform parent, bool active) {
            GameObject go = Addressables.InstantiateAsync(GetAddress(AssetType.PREFAB, assetName), parent).WaitForCompletion();
            go.SetActive(active);
            go.name = finalName;
            return go;
        }

        public static Comp LoadComp(string finalName, string assetName, Transform parent, bool isActive) {
            GameObject go = LoadGo(finalName, assetName, parent, isActive);
            return go.GetComponent<Comp>();
        }

        public static AsyncOperation LoadSceneAsync(string name) {
            return SceneManager.LoadSceneAsync(name);
        }

        [Serializable]
        public enum AssetType {
            ASSET,
            PREFAB,
            INPUTACTIONASSET,
            SOUND,
        }
    }
}