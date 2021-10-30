using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [SerializeField] 
    private WorldStateManager worldStateManager;

    [SerializeField] 
    private Text displayCountdown;

    private int dayCycle;
    private int startNight;
    private float cycleTime;

    private void Update()
    {
        dayCycle = worldStateManager.worldState.getCycleDuration();
        startNight = worldStateManager.worldState.getStartNight();

        cycleTime = worldStateManager.worldState.globalGameTime;
        
        displayCountdown.text = worldStateManager.IsNight()
            ? "Time until day " + (dayCycle - (int) cycleTime)
            : "Time until night " + (startNight - (int) cycleTime);
    }
}
