using LazyPan;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public Transform lookAtTargetTran;

    void Start() {
        if (Data.Instance.TryGetEntityByType((int) GoType.Camera, out uint id)) {
            Entity body = Data.Instance.EntityDic[id];
            lookAtTargetTran = body.Go.UGo.transform;
        }
    }

    private void LateUpdate() {
        if (lookAtTargetTran == null) {
            return;
        }

        transform.LookAt(new Vector3(
            lookAtTargetTran.position.x,
            transform.position.y,
            lookAtTargetTran.position.z));
    }
}