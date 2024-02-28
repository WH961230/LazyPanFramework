using System;
using TMPro;
using UnityEngine;

public class Lab_SaveGame : MonoBehaviour {
    [Serializable]
    public class PlayerData {
        public string PlayerName;
        public int PlayerAge;
        public string PlayerItem;
        public int PlayerMoney;
    }

    public PlayerData playerData;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerAge;
    public TextMeshProUGUI playerItem;
    public TextMeshProUGUI playerMoney;
    private string SAVE_DATA_FILE = "MyData.wanghui";
    void Update() {
        if (playerData != null) {
            playerName.text = playerData.PlayerName;
            playerAge.text = playerData.PlayerAge.ToString();
            playerItem.text = playerData.PlayerItem;
            playerMoney.text = playerData.PlayerMoney.ToString();
        }
    }

    public void Save() {
        SaveDataToJson();
    }

    public void Load() {
        LoadDataFromJson();
    }

    private void SaveDataToJson() {
        Lab_Save.Instance.Save(SAVE_DATA_FILE, playerData);
    }

    private void LoadDataFromJson() {
        PlayerData playerData = Lab_Save.Instance.Load<PlayerData>(SAVE_DATA_FILE);
        this.playerData = playerData;
    }
}