namespace LazyPan {
    public class Behaviour_Move : Behaviour {
        public Behaviour_Move() {
            Input.Instance.Load("Move", Act.Move);
        }
    }
}