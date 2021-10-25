using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    protected State currentState;

	public void setState(State newState) {
        currentState = newState;
    } 
 }
