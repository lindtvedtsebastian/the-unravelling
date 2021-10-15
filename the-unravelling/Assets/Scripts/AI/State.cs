
abstract public class State {
    public abstract void EnterState(State state);
    public abstract void DoState();
    public abstract void LeaveState();
}
