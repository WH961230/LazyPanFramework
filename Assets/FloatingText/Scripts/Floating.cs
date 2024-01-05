using UnityEngine;

public class Floating : MonoBehaviour {
    public Vector3 PositionMult;
    public Vector3 PositionDirection;
    private Vector3 positionTemp;
    private GameObject MainCameraGo;

    void Start() {
        positionTemp = transform.position;
        MainCameraGo = Camera.main.gameObject;
    }

    void Update() {
        positionTemp += PositionDirection * Time.deltaTime;
        PositionDirection += PositionMult * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, positionTemp, 0.5f);
        transform.forward = MainCameraGo.transform.forward;
    }
}