using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Input {
        private InputControls inputControls;
        public static Input Instance;
        public Input() {
            Instance = this;
            inputControls = new InputControls();
        }

        public void Load(string actionName, Action<InputAction.CallbackContext> action) {
            inputControls.Enable();
            inputControls.FindAction(actionName).performed += action;
        }
    }
}