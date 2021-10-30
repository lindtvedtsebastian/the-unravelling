using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class representing the heads up display
/// </summary>
public class HUD : MonoBehaviour {
    [SerializeField] 
    private WorldStateManager worldStateManager;

    [SerializeField] 
    private Text displayCountdown;

    [SerializeField] 
    private Text displayDayCount;

    private int dayCycle;
    private int startNight;
    private float cycleTime;

    private void Update()
    {
        // Get the cycle duration and start night variables from the world state
        // class. Might need to look into a better way of doing this
        dayCycle = worldStateManager.worldState.getCycleDuration();
        startNight = worldStateManager.worldState.getStartNight();

        cycleTime = worldStateManager.worldState.globalGameTime;
        
        // Set the text and color based on the world state manager IsNight function
        displayCountdown.text = worldStateManager.IsNight()
            ? "Time until day : " + (dayCycle - (int) worldStateManager.worldState.globalGameTime)
            : "Time until night : " + (startNight - (int) worldStateManager.worldState.globalGameTime);

        displayCountdown.color = worldStateManager.IsNight() ? Color.white : Color.black;

        displayDayCount.text = "Day cycle : " + (worldStateManager.worldState.currentGameDay + 1);
        displayDayCount.color = worldStateManager.IsNight() ? Color.white : Color.black;
    }
}
