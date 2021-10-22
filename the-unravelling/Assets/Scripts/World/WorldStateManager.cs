using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class WorldStateManager : MonoBehaviour {
    [FormerlySerializedAs("gameState")] public WorldState worldState;
    
    private void Start() {
        InvokeRepeating(nameof(IncrementGameTimeAndDay), 0.0f, 1.0f);
    }

    /// <summary>
    /// Increments a specific world's time and day.
    /// <see cref="WorldState.TickTime()"/>
    /// </summary>
    public void IncrementGameTimeAndDay() {
        worldState.TickTime();
    }
    
    // NOTE: You can create various functions such as this that are utility based. Something such as this could be used for ui if wanted.
    // They are cheap,expandable and expendable.
    public int getCurrentIngameDay() {
        return worldState.currentGameDay;
    }
    public bool IsNight() {
        return worldState.stateOfDay == CycleState.NIGHT;
    }
    public bool IsDay() {
        return worldState.stateOfDay == CycleState.DAY;
    }
} 