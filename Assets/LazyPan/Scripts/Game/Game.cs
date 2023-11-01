using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan.Scripts.Game {
    public class Game : MonoBehaviour {
        private Transform uiRoot;
        private PlayerInput playerInput;
        public GameState gameState;

        void Update() {
            if (gameState == GameState.Fight) {
                playerInput.actions["Setting"].performed += OpenSetting;
            }
        }

        private void OpenSetting(InputAction.CallbackContext callbackContext) {
            Loader.Loader.Load("设置界面", "Interface/UI_Setting", uiRoot);
        }

        [Serializable]
        public enum GameState {
            Launch,
            Loading,
            Fight,
        }
    }
}