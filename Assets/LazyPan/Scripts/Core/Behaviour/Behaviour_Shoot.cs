using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Shoot : Behaviour {
        private Transform ShootTran;

        public Behaviour_Shoot(uint subjectId) : base(subjectId, "Behaviour_Shoot") {
            ShootTran = SubjectComp.Get<Transform>("ShootTran");
            Input.Instance.Load("Player/Shoot", Input_Shoot);
        }

        private void Input_Shoot(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Shoot();
            }
        }

        private void Shoot() {
            if (!IsSelected()) {
                return;
            }

            if (ShootTran == null) {
                return;
            }

            Net Net = SubjectUGo.GetComponent<Net>();
            GameObject selectedObj = UI_Main.Instance.GetSelectedObj();
            Net.CmdShoot(selectedObj, ShootTran.position, ShootTran.position + ShootTran.forward);
        }
    }
}