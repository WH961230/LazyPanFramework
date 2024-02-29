using System;

[Serializable]
public class Lab_SaveConfig {
    [Serializable]
    public class SavePlayerInfo {
        public string PlayerName;
        public int PlayerAge;
        public int PlayerMoney;

        public SavePlayerInfo(string name, int age, int money) {
            PlayerName = name;
            PlayerAge = age;
            PlayerMoney = money;
        }
    }
}