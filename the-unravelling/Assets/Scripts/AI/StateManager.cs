using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public State currentState;
    public Dictionary<int, State> availableStates;

	public StateManager() {
        availableStates = new Dictionary<int, State>();
    }
}
