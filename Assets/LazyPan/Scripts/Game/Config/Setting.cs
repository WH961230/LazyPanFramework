using UnityEngine;

namespace LazyPan {
    [CreateAssetMenu(menuName = "LazyPan/Setting", fileName = "Setting")]
    public class Setting : ScriptableObject {
        public int InstanceID;
        public string TxtPath;
        public string ConfigScriptPath;
    }
}