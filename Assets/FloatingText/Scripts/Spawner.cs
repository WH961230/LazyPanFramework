using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] TextFloat;
    public int index = 0;
    public float LifeTime = 5;

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 300, 30), "Floating Text : " + TextFloat[index].name);
        if (GUI.Button(new Rect(10, 50, 180, 30), "Floating Text")) {
            index = 0;
        }

        if (GUI.Button(new Rect(10, 85, 180, 30), "Floating text with object")) {
            index = 1;
        }

        if (GUI.Button(new Rect(10, 120, 180, 30), "3D text")) {
            index = 2;
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if (TextFloat[index]) {
                    GameObject ob = Instantiate(TextFloat[index], hit.point, Quaternion.identity);
                    Destroy(ob, LifeTime);
                    FloatingText floattext = ob.GetComponentInChildren<FloatingText>();
                    if (floattext != null)
                        floattext.SetText("+" + Random.Range(5, 100));
                }
            }
        }
    }
}