using UnityEngine;

abstract public class State : MonoBehaviour {
    public abstract void EnterState(StateManager stateManager);
    public abstract void DoState();
    public abstract void LeaveState();
}
