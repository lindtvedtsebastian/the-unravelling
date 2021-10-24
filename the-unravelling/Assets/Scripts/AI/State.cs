using UnityEngine;

abstract public class State : MonoBehaviour {
    public abstract void EnterState(State state);
    public abstract void DoState();
    public abstract void LeaveState();
}
