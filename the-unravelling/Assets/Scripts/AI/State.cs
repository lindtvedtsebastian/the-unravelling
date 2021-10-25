using UnityEngine;

abstract public class State : MonoBehaviour {
    protected StateManager _stateManager;

    public abstract void EnterState(StateManager stateManager);
    public abstract void DoState();
    public abstract void LeaveState();
}
