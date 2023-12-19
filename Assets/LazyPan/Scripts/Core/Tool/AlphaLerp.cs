using LazyPan;
using UnityEngine;
using UnityEngine.UI;

public class AlphaLerp : MonoBehaviour {
    public bool Lerp;
    public Image AlphaImg;

    void Start() {
        ClockUtil.Instance.AlarmAfter(6, () => { Lerp = true; });
    }

    void Update() {
        if (Lerp) {
            AlphaImg.color = Color.Lerp(AlphaImg.color,
                new Color(AlphaImg.color.r, AlphaImg.color.g, AlphaImg.color.b, 1), Time.deltaTime * 5);
        }
    }
}