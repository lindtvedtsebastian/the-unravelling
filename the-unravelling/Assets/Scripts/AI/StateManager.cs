using UnityEngine;

/// <summary>
/// A base class for writing StateManagers 
/// </summary>
public class StateManager : MonoBehaviour {
    protected State currentState;

    /// <summary>
    /// Sets a new state 
    /// </summary>
    /// <param name="newState">The state of which to set the current state to</param>
    public void setState(State newState) {
        currentState = newState;
    } 
 }
