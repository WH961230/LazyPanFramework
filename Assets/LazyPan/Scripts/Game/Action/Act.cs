using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Act {
        public static void QuitGame() {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public static void Input_Switch_Behaviour(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Transform select = UI_Main.Instance.UIMainComp.Get<Transform>("UI_Main_BehaviourSelect");
                Transform selectParent = select.parent;
                string[] split = selectParent.name.Split("_");
                int index = int.Parse(split[4]);
                index++;
                index %= 7;
                Transform grid = UI_Main.Instance.UIMainComp.Get<Transform>(string.Concat("UI_Main_Behaviour_Grid_", index.ToString()));
                select.parent = grid;
                select.localPosition = Vector3.zero;
            }
        }

        public static void Input_Switch_Obj(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                SwitchObj(callbackContext.ReadValue<Vector2>());
            }
        }

        public static void SwitchObj(Vector2 scrollVec2) {
            Transform select = UI_Main.Instance.UIMainComp.Get<Transform>("UI_Main_ObjSelect");
            Transform selectParent = select.parent.parent;

            string[] split = selectParent.name.Split("_");
            int index = int.Parse(split[4]);
            if (scrollVec2.y > 0) {
                index--;
                if (index < 0) {
                    index += 7;
                }
            } else if (scrollVec2.y < 0) {
                index++;
            }

            index %= 7;
            Transform grid = UI_Main.Instance.UIMainComp.Get<Transform>(string.Concat("UI_Main_Obj_Icon_", index.ToString()));
            select.parent = grid;
            select.localPosition = Vector3.zero;

            UI_Main.Instance.DisplaySelectObj();
        }
    }
}