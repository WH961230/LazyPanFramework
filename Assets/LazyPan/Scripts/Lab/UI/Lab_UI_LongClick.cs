using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lab_UI_LongClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public Image image;
    public bool isPointerPress;
    void Start() {
        image.fillAmount = 0;
    }

    void Update() {
        if (isPointerPress) {
            image.fillAmount += Time.deltaTime;
        } else {
            image.fillAmount -= Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        isPointerPress = true;
        image.fillAmount = 0;
        Debug.Log("mark");
    }

    public void OnPointerUp(PointerEventData eventData) {
        isPointerPress = false;
    }
}