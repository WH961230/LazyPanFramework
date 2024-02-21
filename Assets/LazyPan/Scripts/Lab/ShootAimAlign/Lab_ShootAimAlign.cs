using LazyPan;
using UnityEngine;
using UnityEngine.UI;
using Input = UnityEngine.Input;

public class Lab_ShootAimAlign : MonoBehaviour {
    public CharacterController CharacterController;
    public RectTransform AimRect;
    public Comp CharacterComp;
    private Image AimImage;
    private RaycastHit hit;
    private Vector3 pointVec;
    private Vector3 tempMousePosition;

    void Start() {
        AimImage = AimRect.GetComponent<Image>();
    }

    void Update() {
        if (Input.GetMouseButton(1)) {
            SetAlpha(AimImage, 1);
            if (tempMousePosition != Input.mousePosition) {
                tempMousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Terrain"))) {
                    pointVec = CharacterComp.Get<Transform>("Point").position;

                    Vector3 worldToScreenPoint = Camera.main.WorldToScreenPoint(hit.point);//击中的点
                    Vector3 pointToScreenVec = Camera.main.WorldToScreenPoint(pointVec);//玩家头部
                    Vector3 hitPointToScreenVec = Camera.main.WorldToScreenPoint(new Vector3(hit.point.x, pointVec.y, hit.point.z));//击中的点到角色头部
                    Vector3 v1 = (worldToScreenPoint - pointToScreenVec).normalized;
                    Vector3 v2 = (hitPointToScreenVec - pointToScreenVec).normalized;

                    Debug.DrawLine(hit.point, pointVec, Color.red);
                    Debug.DrawLine(pointVec, new Vector3(hit.point.x, pointVec.y, hit.point.z), Color.red);
                    Debug.DrawLine(0.5f * (hit.point + pointVec), 0.5f * (new Vector3(hit.point.x, pointVec.y, hit.point.z) + pointVec), Color.magenta);

                    // float angle = Vector3.Angle(v1, v2);
                    // angle = Mathf.Abs(angle);
                    // Vector3 targetAimVec = (hit.point - CharacterController.transform.position).normalized;
                    // Vector3 tempForward = Vector3.ProjectOnPlane(targetAimVec, Vector3.up);
                    // tempForward = Quaternion.AngleAxis(angle, Vector3.Cross(v1, v2).y > 0 ? Vector3.up : Vector3.down) * tempForward;
                    // CharacterController.transform.forward = tempForward;
                    //Debug.Log(angle);

                    CharacterController.transform.LookAt(new Vector3(hit.point.x, CharacterController.transform.position.y, hit.point.z));
                    AimRect.position = worldToScreenPoint;
                }
            }
        } else {
            SetAlpha(AimImage, 0);
        }
    }

    void SetAlpha(Image img, float alpha) {
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }
}