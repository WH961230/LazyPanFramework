using UnityEngine;

public class Lab_UI_Switch : MonoBehaviour {
    [SerializeField] private bool left;
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private float switchSpeed; 
    [SerializeField] private RectTransform block;

    void Start() {
    }

    void Update() {
        if (left) {
            block.anchoredPosition = Vector2.MoveTowards(block.anchoredPosition, new Vector2(leftBound, 0), Time.deltaTime * switchSpeed);
        } else {
            block.anchoredPosition = Vector2.MoveTowards(block.anchoredPosition, new Vector2(rightBound, 0), Time.deltaTime * switchSpeed);
        }
    }

    public void Switch() {
        left = !left;
    }
}