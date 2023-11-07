using System.IO;
using LazyPan;
using UnityEngine;

public class ReadCSV : MonoBehaviour {
    public string content;
    public string[] lines;

    private void Read(string fileName) {
        using (StreamReader sr = new StreamReader(Application.dataPath + Game.Instance.Setting.FilePath + fileName + ".csv")) {
            string str = null;
            string line;
            while ((line = sr.ReadLine()) != null) {
                str += line + '\n';
            }

            content = str.TrimEnd('\n');
            lines = content.Split('\n');
        }
    }

    public T Get<T>(string fileName, string key) {
        Read(fileName);

        for (int i = 0; i < lines.Length; i++) {
            string[] line = lines[i].Split(',');
            if (line[0] == key) {
                return (T) System.Convert.ChangeType(line[1], typeof(T));
            }
        }

        return default(T);
    }
}