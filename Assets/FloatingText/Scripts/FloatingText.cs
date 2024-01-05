using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingText : MonoBehaviour
{
    public TextMesh textMesh;
    public float LifeTime = 1;
    public bool FadeEnd = false;
    public Color TextColor = Color.white;

    private float alpha = 1;
    private float timeTemp = 0;

    private void Awake()
    {
        textMesh = this.GetComponent<TextMesh>();
    }

    void Start()
    {
        timeTemp = Time.time;
        GameObject.Destroy(this.gameObject, LifeTime);
    }

    public void SetText(string text)
    {
        if (textMesh)
            textMesh.text = text;
    }

    void Update()
    {
        if (FadeEnd)
        {
            if (Time.time >= ((timeTemp + LifeTime) - 1))
            {
                alpha = 1.0f - (Time.time - ((timeTemp + LifeTime) - 1));
            }
        }

        textMesh.color = new Color(TextColor.r, TextColor.g, TextColor.b, alpha);

        if (Camera.current != null)
        {
            this.transform.localScale = Vector3.one;
            Quaternion rota = Quaternion.LookRotation((this.transform.position - Camera.current.transform.position).normalized);
            this.transform.rotation = rota;
        }
        
    }
}
