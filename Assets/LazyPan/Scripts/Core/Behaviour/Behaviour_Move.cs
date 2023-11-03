using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Move : Behaviour {
        public Go go;
        public Vector2 vec;
        public Behaviour_Move(int id) {
            go = Data.Instance.go[id];

            InputControls inputControls = new InputControls();
            inputControls.Enable();
            inputControls.Player.Move.performed += Move;
            Data.Instance.OnUpdateEvent?.AddListener(Update_Move);
        }

        private void Update_Move() {
            go.UGo.transform.Translate(new Vector3(vec.x, 0, vec.y) * Time.deltaTime);
        }

        private void Move(InputAction.CallbackContext callbackContext) {
            vec = callbackContext.ReadValue<Vector2>();
        }
    }
}