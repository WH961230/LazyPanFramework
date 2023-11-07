namespace LazyPan {
    public class Config {
        private ReadCSV readCSV;
        public static Config Instance;
        public Config() {
            Instance = this;
            readCSV = new ReadCSV();
        }

        public T Get<T>(string fileName, string key) {
            return readCSV.Get<T>(fileName, key);
        }
    }
}