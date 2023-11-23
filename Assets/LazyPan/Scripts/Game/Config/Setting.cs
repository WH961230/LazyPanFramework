using UnityEngine;

namespace LazyPan {
    [CreateAssetMenu(menuName = "LazyPan/Setting", fileName = "Setting")]
    public class Setting : ScriptableObject {
        public uint InstanceID;
        public string TxtPath;
        public string ConfigScriptPath;
    }
}