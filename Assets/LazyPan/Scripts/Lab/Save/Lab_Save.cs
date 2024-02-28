using System;
using System.IO;
using LazyPan;
using UnityEngine;

public class Lab_Save : Singleton<Lab_Save> {
    public void Save(string saveFileName, object data) {
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        try {
            File.WriteAllText(path, json);

            #if UNITY_EDITOR
            Debug.LogFormat("存储成功！路径：{0}", path);
            #endif

        } catch (Exception e) {
            Debug.LogFormat("错误! 信息:{0}", e.Message);
        }
    }

    public T Load<T>(string loadFileName) {
        string path = Path.Combine(Application.persistentDataPath, loadFileName);
        try {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        } catch (Exception e) {
            Debug.LogFormat("错误! 信息:{0}", e.Message);
            throw;
        }
        
    }
}
