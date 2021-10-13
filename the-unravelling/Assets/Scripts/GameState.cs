using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: revisit if these enums are even worth having at all.
public enum CycleState {
     DAY = 0,
     NIGHT = 1
 }
    
// NOTE: Could be used to "trigger" story cutscenes or starting cutscene perhaps.

[Serializable]
public class GameState {
    public float globalGameTime; // should be 0 if it's a new game and should be gotten from a save in other cases.
    public float Tick = 1.0f;
    public int currentGameDay; 
    public bool _isGameOver;
    
    public CycleState _stateOfDay;

    /// <summary>
    ///  This function keeps track of the game days and whether it is currently night or day.
    ///  It is ran in the Start method every second. 
    /// </summary>
    public void UpdateTime() { 
        globalGameTime += Tick;
        _stateOfDay = globalGameTime > 1200 ? CycleState.NIGHT : CycleState.DAY;
         
        if (globalGameTime > 1800) {
            currentGameDay++;
            globalGameTime = 0;
        }
        Debug.Log(globalGameTime.ToString());
        var dayOrNight = (int)_stateOfDay;
        Debug.Log(dayOrNight.ToString());
    }
}











