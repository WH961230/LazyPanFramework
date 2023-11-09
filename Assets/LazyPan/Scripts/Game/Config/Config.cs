namespace LazyPan {
    public class Config : Singleton<Config> {
        public void Init() {
            UIConfig.Init();
        }
    }
}