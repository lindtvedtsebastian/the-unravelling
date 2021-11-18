using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class WorldStateManager : MonoBehaviour {
    [FormerlySerializedAs("gameState")] public WorldState worldState;
    public GameObject NightEffect;
    
    private void Start() {
        worldState = GameData.Get.world.state;
        InvokeRepeating(nameof(IncrementGameTimeAndDay), 0.0f, 1.0f);
		NightEffect = GameObject.FindWithTag("NightEffect");
    }

    /// <summary>
    /// Increments a specific world's time and day.
    /// <see cref="WorldState.TickTime()"/>
    /// </summary>
    public void IncrementGameTimeAndDay() {
        worldState.TickTime();
        NightEffect.SetActive(IsNight());
    }
    
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
