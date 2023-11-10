namespace LazyPan {
    public class Config : Singleton<Config> {
        public void Init() {
            ObjConfig.Init();
            UIConfig.Init();
        }
    }
}