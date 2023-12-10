using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Hit : Behaviour {
        private bool isHit;

        public Behaviour_Hit(uint subjectId) : base(subjectId, "Behaviour_Hit") {
            Input.Instance.Load("Player/Hit", Input_Behaviour_Hit);
        }

        private void Input_Behaviour_Hit(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Hit();
                SubjectComp.Get<Animator>("Animator").SetTrigger("Hit");
            }
        }

        private void Hit() {
            isHit = true;
            Sound.Instance.PlaySound("WoodHit", SubjectUGo.transform.position, false);
            Collider[] hitCollider = Physics.OverlapBox(SubjectComp.Get<GameObject>("Head").transform.position, Vector3.one);
            if (hitCollider.Length > 0) {
                for (int i = 0; i < hitCollider.Length; i++) {
                    if (hitCollider[i].isTrigger) {
                        continue;
                    }

                    int instanceID = hitCollider[i].gameObject.GetInstanceID();
                    if (!Data.Instance.GetEntityByInstanceID(instanceID, out Entity entity)) {
                        continue;
                    }

                    if (entity == null && entity.GoInstanceID == SubjectUGo.GetInstanceID()) {
                        continue;
                    }

                    if (entity.Health == 0 || entity.isLocalMainPlayer) {
                        continue;
                    }

                    entity.Damage(1, () => {
                        Comp comp = entity.Go.UGo.GetComponent<Comp>();
                        Transform health = comp.Get<Transform>("Health");
                        RectTransform healthRect = (RectTransform) health;
                        float healthRatio = (float) entity.Health / ObjConfig.Get(entity.GoSign).Health;
                        healthRect.sizeDelta = new Vector2(healthRatio, healthRect.sizeDelta.y);
                    });
                }
            }
        }
    }
}