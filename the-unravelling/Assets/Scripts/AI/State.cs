using System;
using UnityEngine;

/// <summary>
/// Base class for creating states 
/// </summary>
abstract public class State : MonoBehaviour {
    protected StateManager _stateManager;

    protected World _world;

    void Start() {
        _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
    }

    /// <summary>
    /// The "Constructor" of the state 
    /// </summary>
    public abstract void EnterState(StateManager stateManager);

    /// <summary>
    /// The "Update" of the state 
    /// </summary>
    public abstract void DoState();

    /// <summary>
    /// The "Destructor" of the state
    /// </summary>
    public abstract void LeaveState();
}
