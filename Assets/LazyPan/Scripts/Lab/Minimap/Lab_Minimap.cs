using UnityEngine;

public class Lab_Minimap : MonoBehaviour {
    public bool isPlayer;
    public bool isMiniPlayer;
    public GameObject Player;
    public Transform TempPos;
    public float speed;

    void Update() {
        if (isPlayer) {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        if (isMiniPlayer) {
            Vector3 playerOffVec = Player.transform.position - TempPos.position;
            Vector2 playerOffXZ = new Vector2(playerOffVec.x, playerOffVec.z);
            GetComponent<RectTransform>().anchoredPosition = 6 * new Vector2(playerOffXZ.x, playerOffXZ.y);
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -Player.transform.eulerAngles.y);
        }
    }
}