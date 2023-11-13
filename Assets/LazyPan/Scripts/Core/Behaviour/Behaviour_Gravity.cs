using UnityEngine;

namespace LazyPan {
    public class Behaviour_Gravity : Behaviour {
        private float GravityValue;
        private Vector3 moveDir;
        private CharacterController controller;

        public Behaviour_Gravity(int subjectId) : base(subjectId) {
            Data.Instance.OnUpdateEvent?.AddListener(Update_Behaviour_Gravity);
            controller = SubjectGo.UGo.GetComponent<Comp>().Get<CharacterController>("CharacterController");
            GravityValue = float.Parse(BehaviourConfig.Get("Behaviour_Gravity").Parameter);
        }

        private void Update_Behaviour_Gravity() {
            if (controller != null) {
                if (controller.gameObject.transform.position.y <= 0) {
                    moveDir.y = 0f;
                }
                moveDir.y += -GravityValue * Time.deltaTime;
                controller.Move(moveDir * Time.deltaTime);
            }
        }
    }
}