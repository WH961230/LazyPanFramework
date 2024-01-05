using UnityEngine;

public class Lab_FloatingText : MonoBehaviour {
    public float LifeTime;
    public GameObject TextFloat;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if (TextFloat) {
                    GameObject ob = Instantiate(TextFloat, hit.point, Quaternion.identity);
                    Destroy(ob, LifeTime);
                    FloatingText floattext = ob.GetComponentInChildren<FloatingText>();
                    if (floattext != null) {
                        floattext.SetText("+" + Random.Range(5, 100));
                    }
                }
            }
        }
    }
}