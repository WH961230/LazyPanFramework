using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Move : Behaviour {
        public Go go;
        public Vector2 vec;
        public Behaviour_Move(int id) {
            go = Data.Instance.go[id];
            Input.Instance.Load("Player/Move", Move);
            if (Data.Instance.goFunc.TryGetValue(id, out List<Behaviour> list)) {
                list.Add(this);
                Data.Instance.goFunc[id] = list;
            } else {
                List<Behaviour> behaviourList = new List<Behaviour>();
                behaviourList.Add(this);
                Data.Instance.goFunc.Add(id, behaviourList);
            }
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