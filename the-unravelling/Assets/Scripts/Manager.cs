using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    
    // NOTE: Perhaps the day night cycle can last 15 minutes. Decent balance between mining/defending/lack of action?
    
    private float globalGameTime;
    private float tick = 1.0f;
    private int currentGameDay;
    private bool shouldTurnToNight;

    private enum CycleState {
        DAY = 0,
        NIGHT = 1,
    }
    
    // NOTE: 
    private enum GameState {
        
        GAME_OVER = 0,
    }

    public float getGlobalGameTime() {
        return globalGameTime;
    }
    
    public float getCurrentGameDay() {
        return currentGameDay;
    }

    public void PauseGame() {
        Time.timeScale = 0.0f;
        // AudioListener.pause = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1.0f;
        // AudioListener.pause = false;
    }

    
    // Update is called once per frame
    void Update() {
        globalGameTime += tick;
        if (globalGameTime >= 10000) {
            currentGameDay++;
        }
    }
}
