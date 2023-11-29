using LazyPan;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public Transform lookAtTargetTran;
    void Start() {
        if (Data.Instance.TryGetDataBodyByType((int) GoType.Camera, out uint id)) {
            DataBody body = Data.Instance.dataBodyDic[id];
            lookAtTargetTran = body.Go.UGo.transform;
        }
    }

    void Update() {
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