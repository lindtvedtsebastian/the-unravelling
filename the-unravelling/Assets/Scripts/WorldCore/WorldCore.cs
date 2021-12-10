using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World core class
/// </summary>
public class WorldCore : UnitRegeneration { 
    
    [SerializeField]public bool isLastDay = false;
    
    public Animator anim;
    public GameOverScreen GameOverScreen;
    public WinningScreen WinningScreen;
    
    //Cashed property index
    private static readonly int IsGameFinished = Animator.StringToHash("isGameFinished");

    protected override void Awake() {
        base.Awake();
        // Set start-health to 1000
        maxHealth = 1000;
        
        //Get animations
        anim = GetComponent<Animator>();
    }


    /// <summary>
    /// Run every frame
    /// </summary>
    private void Update() {
        onLastDay();
    }

    /// <summary>
    /// Spawns portal
    /// </summary>
    public void onLastDay() {
        anim.SetBool(IsGameFinished, isLastDay);
    }

    /// <summary>
    ///  If portal has spawned and the player touches the portal
    ///  the game is won. 
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && isLastDay) {
            WinningScreen.Setup();
        }
    }
    
    
    /// <summary>
    /// When destroyed displays game over screen.
    /// </summary>
    private void Destroy() {
        GameOverScreen.Setup(0, "The world core was destroyed");
    }
    
}
