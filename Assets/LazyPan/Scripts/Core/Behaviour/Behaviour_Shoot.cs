using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Shoot : Behaviour {
        private Transform ShootPoint;

        public Behaviour_Shoot(int subjectId) : base(subjectId) {
            ShootPoint = SubjectComp.Get<Transform>("ShootPoint");
            Input.Instance.Load("Player/Shoot", Input_Shoot);
        }

        private void Input_Shoot(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Shoot();
            }
        }

        private void Shoot() {
            if (ShootPoint == null) {
                return;
            }
            var bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.localScale /= 5;
            bullet.transform.position = ShootPoint.position;
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            rb.AddForce(ShootPoint.forward * 10f, ForceMode.Impulse);
        }
    }
}