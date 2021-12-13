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

    public CycleState stateOfDay;
    public bool regenerateResource = false;

    private const int cycleDuration = 240;
    private const int startNight = 120;

    public WorldState() {
        currentGameDay = 0;
        globalGameTime = 0;
	}

	void OnEnable() {
	}

	public int getCycleDuration()
	{
		return cycleDuration;
	}

	public int getStartNight()
	{
		return startNight;
	}

	/// <summary>
    ///  This function keeps track of the game days and whether it is currently night or day.
    ///  It is ran in the Start method every second. 
    /// </summary>
    public void TickTime() {
        globalGameTime += Tick;
        stateOfDay = globalGameTime > startNight ? CycleState.NIGHT : CycleState.DAY;

        if (globalGameTime > cycleDuration) {
	        currentGameDay++;
            globalGameTime = 0;
            regenerateResource = true;
        }
	}
}
