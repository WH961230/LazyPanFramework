using UnityEngine;
using Input = LazyPan.Input;

public class Lab_Input : MonoBehaviour {
    void Start() {
        Input.Instance.Load(Input.Space, (obj) => {
            string log = "";
            log += $"obj.started: {obj.started} \t";
            log += $"obj.performed: {obj.performed} \t";
            log += $"obj.canceled: {obj.canceled} \t";
            log += $"obj.duration: {obj.duration}";
            Debug.Log(log);
        });
    }

    void Update() {
    }
}