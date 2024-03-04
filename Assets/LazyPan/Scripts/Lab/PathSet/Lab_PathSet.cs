using LazyPan;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.Events;

public class Lab_PathSet : MonoBehaviour {
    public Comp PlayerComp;
    void Start() {
        SetPath("PathA", () => {
            SetPath("PathB", null);
        });
    }

    void SetPath(string pathSign, UnityAction action) {
        PathCreator creator = PlayerComp.Get<Transform>(pathSign).GetComponent<PathCreator>();
        PathFollower follower = PlayerComp.GetComponent<PathFollower>();
        follower.pathCreator = creator;
        follower.Init();
        follower.StopAction = action;
        Debug.LogFormat("设置路线 {0}", pathSign);
    }
}