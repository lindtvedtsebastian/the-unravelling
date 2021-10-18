using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WorldStateManager : MonoBehaviour {
    [FormerlySerializedAs("gameState")] public WorldState worldState;

    private void Start() {
        InvokeRepeating(nameof(IncrementGameTimeAndDay), 0.0f, 1.0f);
    }

    /// <summary>
    /// Increments a world's time and day.
    /// <see cref="WorldState.TickTime()"/>
    /// </summary>
    public void IncrementGameTimeAndDay() {
        worldState.TickTime();
    }

    public bool IsNight() {
        return worldState.stateOfDay == CycleState.NIGHT;
    }
    public bool IsDay() {
        return worldState.stateOfDay == CycleState.DAY;
    }
} 