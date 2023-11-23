namespace LazyPan {
    public partial class NetManager {
        public static ConnectType ConnectType;
        public static bool IsClient => ConnectType == ConnectType.Client;
        public static bool IsServer => ConnectType == ConnectType.Server;
        public static bool IsHost => ConnectType == ConnectType.Host;
    }
}