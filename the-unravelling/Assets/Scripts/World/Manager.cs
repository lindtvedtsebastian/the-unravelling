using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    private float _globalGameTime; // should be 0 if it's a new game and should be gotten from a save in other cases.
    private const float Tick = 1.0f;
    private int _currentGameDay;
    private bool _isGameOver;
    
    private CycleState _stateOfDay;
    private GameState _gameState;

    private static Manager _instance;

    public static Manager Instance {
        get {
            if (_instance == null) {
                Debug.LogError("game manager is null");
            }
            return _instance;
        }
    }
    private void Awake() {
        _instance = this;
    }

    private void Start() {
        InvokeRepeating("UpdateGame", 0.0f, 1.0f);
    }

     /// <summary>
     ///  This function keeps track of the game days and whether it is currently night or day.  
     /// </summary>
    void UpdateGame() { 
        _globalGameTime += Tick;
        if (_globalGameTime > 10) {
            _currentGameDay++;
            _globalGameTime = 0;
        }

        if (_globalGameTime > 5) {
            _stateOfDay = CycleState.NIGHT;
        }
        else {
            _stateOfDay = CycleState.DAY;
        }
        Debug.Log(_globalGameTime.ToString());
        var floorTypeInt = (int)_stateOfDay;
        Debug.Log(floorTypeInt.ToString());
     }
}

// NOTE: revisit if these enums are even worth having at all.
public enum CycleState {
     DAY = 0,
     NIGHT = 1
 }
    
public enum GameState {
    GAME_WIN,
    GAME_OVER
}