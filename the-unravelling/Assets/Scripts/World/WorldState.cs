using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

public enum CycleState {
    DAY = 0,
    NIGHT = 1
}

[Serializable]
public class WorldState {
    public float globalGameTime;
	public float Tick = 1.0f;
    public int currentGameDay;
    //public bool isGameOver;
    
    public CycleState stateOfDay;

    private const int dayDuration = 1800;
    private const int nightDuration = 1200;

	public WorldState() {
        currentGameDay = 0;
        globalGameTime = 0;
    }

    /// <summary>
    ///  This function keeps track of the game days and whether it is currently night or day.
    ///  It is ran in the Start method every second. 
    /// </summary>
    public void TickTime() {
        globalGameTime += Tick;
        stateOfDay = globalGameTime > nightDuration ? CycleState.NIGHT : CycleState.DAY;

        if (globalGameTime > dayDuration) {
            currentGameDay++;
            globalGameTime = 0;
		}
	}
}
