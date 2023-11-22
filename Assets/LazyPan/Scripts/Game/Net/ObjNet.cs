using Mirror;
using UnityEngine;

namespace LazyPan {
    public class ObjNet : NetworkBehaviour {
        public override void OnStartLocalPlayer() {
            base.OnStartLocalPlayer();
            Debug.Log("OnStartLocalPlayer: " + GetComponent<Comp>().ObjSign);
        }

        public override void OnStartAuthority() {
            base.OnStartAuthority();
            Debug.Log("OnStartAuthority: " + GetComponent<Comp>().ObjSign);
        }
    }
}