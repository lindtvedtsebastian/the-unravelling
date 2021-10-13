using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public GameState gameState;
    
    private void Start() {
        InvokeRepeating(nameof(IncrementGameTimeAndDay), 0.0f, 1.0f);
    }

    public void IncrementGameTimeAndDay() {
        gameState.UpdateTime();
    }
} 