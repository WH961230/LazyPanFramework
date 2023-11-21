using System.Collections.Generic;
using Mirror;

namespace LazyPan {
    public class Net : NetworkManager {
        public List<ObjNet> ObjNets = new List<ObjNet>();

        public override void Update() {
            base.Update();
            UpdateNet();
        }

        private void UpdateNet() {
            ObjNets.Clear();
            // RoleNet[] allRoleNet = MyButtleLayer.RoleLayer.GetComponentsInChildren<RoleNet>(true);
            // if (MyButtleLayer == null || MyButtleLayer.RoleLayer == null) {
            //     return;
            // }
            //
            // for (int i = 0; i < allRoleNet.Length; i++) {
            //     if (allRoleNet[i].MyGameWorld != null && allRoleNet[i].WorldId == gameWorld.WorldId) {
            //         RoleNetList.Add(allRoleNet[i]);
            //     }
            // }
        }
    }
}