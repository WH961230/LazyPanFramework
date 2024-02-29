using TMPro;
using UnityEngine;

public class Lab_SaveGame : MonoBehaviour {
    private string SAVE_DATA_FILE = "MyData.wanghui";
    public TextMeshProUGUI st;
    public TextMeshProUGUI it;
    public TextMeshProUGUI fl;

    public Lab_SaveConfig.SavePlayerInfo addPlayerInfo;
    public string loadPlayerNameInfo;
    
    private void Start() {
    }

    public void Save() {
        SaveDataToJson();
    }

    public void Load() {
        LoadDataFromJson();
    }

    public void Add() {
        Lab_SaveData.Instance.SavePlayerInfoDatas.Add(addPlayerInfo);
    }

    private void SaveDataToJson() {
        string info = "";
        foreach (var VARIABLE in Lab_SaveData.Instance.SavePlayerInfoDatas) {
            info += VARIABLE.PlayerName + " " + VARIABLE.PlayerAge + " " + VARIABLE.PlayerMoney + "\n\t";
        }
        Debug.Log(info);
        Lab_Save.Instance.Save(SAVE_DATA_FILE, Lab_SaveData.Instance);
    }

    private void LoadDataFromJson() {
        Lab_SaveData saveConfig = Lab_Save.Instance.Load<Lab_SaveData>(SAVE_DATA_FILE);
        if (saveConfig != null) {
            foreach (var data in saveConfig.SavePlayerInfoDatas) {
                if (data.PlayerName == loadPlayerNameInfo) {
                    st.text = data.PlayerName;
                    it.text = data.PlayerAge.ToString();
                    fl.text = data.PlayerMoney.ToString();
                }
            }
        }
    }
}