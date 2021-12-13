using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class representing the heads up display
/// </summary>
public class HUD : MonoBehaviour {
    private WorldState _worldState;

    [SerializeField]
    private Text displayCountdown;

    [SerializeField] 
    private Text displayDayCount;

    private int dayCycle;
    private int startNight;
    private float cycleTime;

    private void Start() {
        _worldState = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world.state;
    }

    private void Update()
    {
        // Get the cycle duration and start night variables from the world state
        // class. Might need to look into a better way of doing this
        dayCycle = _worldState.getCycleDuration();
        startNight = _worldState.getStartNight();

        cycleTime = _worldState.globalGameTime;
        
        // Set the text and color based on the world state manager IsNight function
        displayCountdown.text = _worldState.IsNight()
            ? "Time until day : " + (dayCycle - (int) _worldState.globalGameTime)
            : "Time until night : " + (startNight - (int) _worldState.globalGameTime);

        displayCountdown.color = _worldState.IsNight() ? Color.white : Color.black;

        displayDayCount.text = "Day cycle : " + (_worldState.currentGameDay + 1);
        displayDayCount.color = _worldState.IsNight() ? Color.white : Color.black;
    }
}
