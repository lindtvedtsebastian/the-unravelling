using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

// NOTE: revisit if these enums are even worth having at all.
public enum CycleState {
    //MORNING = 0,
    DAY = 1,
    NIGHT = 2
    //DUSK = 3,
    
 }

[Serializable]
public class WorldState {
    public float globalGameTime; // should be 0 if it's a new game and should be gotten from a save in other cases.
    public float Tick = 1.0f;
    public int currentGameDay;
    //public bool isGameOver;
    
    public CycleState stateOfDay;

    private const int dayDuration = 1800;
    private const int nightDuration = 1200;

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
        /*    Debug.Log(globalGameTime.ToString());
      var dayOrNight = (int)stateOfDay;
        string myString = null;
        switch(dayOrNight) { // hack to just not have to look at int for day/night
            case 1: 
                myString = "DAY"; 
                break; 
            case 2: 
                myString = "NIGHT"; 
                break;
        }
        Debug.Log(myString);*/
    }
}











