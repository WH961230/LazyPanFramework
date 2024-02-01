using UnityEngine;
using UnityEngine.EventSystems;

public class Lab_UI_PointEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject PointEnterUI;
    void Start() {
        PointEnterUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        PointEnterUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        PointEnterUI.SetActive(false);
    }
}