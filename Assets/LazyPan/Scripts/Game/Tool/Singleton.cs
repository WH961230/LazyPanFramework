namespace LazyPan {
    public class Singleton<T> where T : new() {
        static T instance;

        public static T Instance {
            get {
                if (instance != null) {
                    return instance;
                }

                instance = new T();
                return instance;
            }
        }
    }
}