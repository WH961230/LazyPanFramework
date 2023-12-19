using UnityEngine;

namespace LazyPan {
    public class LaunchLab : MonoBehaviour {
        private void Start() {
            Loader.LoadGo("主相机", "Obj/Obj_MainCamera", null, true);
            Loader.LoadGo("灯光", "Obj/Obj_MainDirectionalLight", null, true);
            Transform uiRoot = Loader.LoadGo("画布", "Global/Global_UI_Root", null, true).transform;
            Comp uiLabUIComp = Loader.LoadComp("实验室UI列表", "UI/Lab/UI_Lab_UI", uiRoot, true);
            Texture2D cursorTexture = Loader.LoadAsset<Texture2D>(Loader.AssetType.SPRITE, "Cursor");
            Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.Auto);
        }
    }
}