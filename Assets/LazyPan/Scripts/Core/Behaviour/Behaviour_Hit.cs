using UnityEngine;
using UnityEngine.InputSystem;

namespace LazyPan {
    public class Behaviour_Hit : Behaviour {
        private bool isHit;

        public Behaviour_Hit(uint subjectId) : base(subjectId) {
            Input.Instance.Load("Player/Hit", Input_Behaviour_Hit);
        }

        private void Input_Behaviour_Hit(InputAction.CallbackContext callbackContext) {
            if (callbackContext.performed) {
                Hit();
            }
        }

        private void Hit() {
            isHit = true;
            Sound.Instance.PlaySound("WoodHit", new Sound.SoundInfo(SubjectUGo.transform.position));
            Collider[] hitCollider =
                Physics.OverlapBox(SubjectComp.Get<GameObject>("Head").transform.position, Vector3.one);
            if (hitCollider.Length > 0) {
                for (int i = 0; i < hitCollider.Length; i++) {
                    if (hitCollider[i].isTrigger) {
                        continue;
                    }

                    int instanceID = hitCollider[i].gameObject.GetInstanceID();
                    DataBody dataBody = Data.Instance.GetDataBodyByInstanceID(instanceID);
                    if (dataBody == null && dataBody.GoInstanceID == SubjectUGo.GetInstanceID()) {
                        continue;
                    }

                    if (dataBody.Health == 0 || dataBody.isLocalMainPlayer) {
                        continue;
                    }

                    dataBody.Damage(1, () => {
                        Transform health = dataBody.Go.UGo.GetComponent<Comp>().Get<Transform>("Health");
                        RectTransform healthRect = (RectTransform)health;
                        healthRect.sizeDelta = new Vector2(healthRect.sizeDelta.x - 0.5f, healthRect.sizeDelta.y);
                    });
                }
            }
        }
    }
}