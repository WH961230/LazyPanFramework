﻿using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Act {
        public static void QuitGame() {
            Debug.Log("QuitGame");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public static void Input_Switch_Behaviour(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Transform select = UI.Instance.Get("UI_Main").Get<Transform>("UI_Main_Behaviour_Grid_Select");
                Transform selectParent = select.parent;
                string[] split = selectParent.name.Split("_");
                int index = int.Parse(split[4]);
                index++;
                index %= 7;
                Transform grid = UI.Instance.Get("UI_Main").Get<Transform>(string.Concat("UI_Main_Behaviour_Grid_", index.ToString()));
                select.parent = grid;
                select.localPosition = Vector3.zero;
            }
        }

        public static void Input_Switch_Obj(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Transform select = UI.Instance.Get("UI_Main").Get<Transform>("UI_Main_Obj_Grid_Select");
                Transform selectParent = select.parent;
                string[] split = selectParent.name.Split("_");
                int index = int.Parse(split[4]);
                index++;
                index %= 7;
                Transform grid = UI.Instance.Get("UI_Main").Get<Transform>(string.Concat("UI_Main_Obj_Grid_", index.ToString()));
                select.parent = grid;
                select.localPosition = Vector3.zero;
            }
        }
    }
}