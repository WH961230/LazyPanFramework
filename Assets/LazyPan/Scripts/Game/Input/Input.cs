using System;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Input : Singleton<Input> {
        private InputControls inputControls = new InputControls();
        public void Load(string actionName, Action<InputAction.CallbackContext> action) {
            inputControls.Enable();
            inputControls.FindAction(actionName).started += action;
            inputControls.FindAction(actionName).performed += action;
            inputControls.FindAction(actionName).canceled += action;
        }
    }
}