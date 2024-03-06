using System;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Input : Singleton<Input> {
        public static string Space = "Player/Space";
        private InputControls inputControls = new InputControls();

        public void Load(string actionName, Action<InputAction.CallbackContext> action) {
            inputControls.Enable();
            inputControls.FindAction(actionName).started += action;
            inputControls.FindAction(actionName).performed += action;
            inputControls.FindAction(actionName).canceled += action;
        }

        public void UnLoad(string actionName, Action<InputAction.CallbackContext> action) {
            if (inputControls == null) { 
                inputControls = new InputControls();
            }
            inputControls.Enable();
            inputControls.FindAction(actionName).started -= action;
            inputControls.FindAction(actionName).performed -= action;
            inputControls.FindAction(actionName).canceled -= action;
        }
    }
}