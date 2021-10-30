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

    private int timeLeftDay;
    private int timeLeftNight;
    private float cycleTime;

    private int countdown;
    
    private void Start()
    {
        //cycleTime = worldStateManager.worldState.globalGameTime;
        timeLeftDay = worldStateManager.worldState.getDayDuration();
        timeLeftNight = worldStateManager.worldState.getNightDuration();
    }

    private void Update()
    {
        timeLeftDay = worldStateManager.worldState.getDayDuration();
        cycleTime = worldStateManager.worldState.globalGameTime;
        countdown = timeLeftDay - (int)cycleTime;
        displayCountdown.text ="Time until night " + countdown;
        //Debug.Log(worldStateManager.worldState.globalGameTime);
    }
}
