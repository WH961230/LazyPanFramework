using System.Collections.Generic;

namespace LazyPan {
    public partial class Data : Singleton<Data> {
        public bool TryGetDataBodyByType(int type, out int id) {
            Dictionary<int,DataBody>.ValueCollection valueCollection = dataBodyDic.Values;
            foreach (var body in dataBodyDic.Values) {
                if (type == body.Type) {
                    id = body.ID;
                    return true;
                }
            }

            id = default;
            return false;
        }
    }
}