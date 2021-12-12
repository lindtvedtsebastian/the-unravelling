using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class WorldStateManager : MonoBehaviour {
    private World _world;
    public GameObject NightEffect;

    private void Start() {
        _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
        InvokeRepeating(nameof(IncrementGameTimeAndDay), 0.0f, 1.0f);
		NightEffect = GameObject.FindWithTag("NightEffect");
    }

    /// <summary>
    /// Increments a specific world's time and day.
    /// <see cref="WorldState.TickTime()"/>
    /// </summary>
    public void IncrementGameTimeAndDay() {
        _world.state.TickTime();
        NightEffect.SetActive(IsNight());
    }
    
    public int getCurrentIngameDay() {
        return _world.state.currentGameDay;
    }
	
    public bool IsNight() {
        return _world.state.stateOfDay == CycleState.NIGHT;
    }

    public bool IsDay() {
        return _world.state.stateOfDay == CycleState.DAY;
    }

    public World getWorld() {
        return _world;
    }
} 
