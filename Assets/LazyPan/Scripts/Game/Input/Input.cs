namespace LazyPan {
    public class Input {
        public static Input Instance;
        public void Start() {
            Instance = this;
        }

        public void Load() {
            InputControls inputControls = new InputControls();
            inputControls.Enable();
            inputControls.UI.Setting.performed += Act.OpenSetting;
        }
    }
}