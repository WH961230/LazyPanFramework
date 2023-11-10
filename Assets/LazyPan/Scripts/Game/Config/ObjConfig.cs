using System;
using System.Collections.Generic;

namespace LazyPan {
    public class ObjConfig {
		public string Sign;
		public int Type;
		public string Name;

        private static bool isInit;
        private static string content;
        private static string[] lines;
        private static Dictionary<string, ObjConfig> dics = new Dictionary<string, ObjConfig>();

        public ObjConfig(string line) {
            try {
                string[] values = line.Split(',');
				Sign = values[0];
				Type = int.Parse(values[1]);
				Name = values[2];

            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void Init() {
            if (isInit) {
                return;
            }
            ReadCSV.Instance.Read("ObjConfig", out content, out lines);
            dics.Clear();
            for (int i = 0; i < lines.Length; i++) {
                if (i > 2) {
                    ObjConfig config = new ObjConfig(lines[i]);
                    dics.Add(config.Sign, config);
                }
            }

            isInit = true;
        }

        public static ObjConfig Get(string sign) {
            if (dics.TryGetValue(sign, out ObjConfig config)) {
                return config;
            }

            return null;
        }

        public static List<string> GetKeys() {
              if (!isInit) {
                   Init();
              }
              var keys = new List<string>();
              keys.AddRange(dics.Keys);
              return keys;
        }
    }
}