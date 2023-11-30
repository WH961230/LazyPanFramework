using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Hit : Behaviour {
        public Behaviour_Hit(uint subjectId) : base(subjectId) {
            SubjectComp.OnTriggerEnterEvent?.AddListener(OnTriggerEnter);
            Input.Instance.Load("Player/Hit", Input_Behaviour_Hit);
        }

        private void Input_Behaviour_Hit(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Hit();
            }
        }

        private void Hit() {
            Debug.Log("挥击");
            Sound.Instance.PlaySound("WoodHit", new Sound.SoundInfo(SubjectUGo.transform.position));
        }

        private void OnTriggerEnter(Collider collider) {
            Debug.Log("挥击触发");
        }
    }
}