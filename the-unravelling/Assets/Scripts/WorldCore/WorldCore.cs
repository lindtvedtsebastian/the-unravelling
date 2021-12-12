using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World core class
/// </summary>
public class WorldCore : UnitRegeneration { 
    
    private bool isLastDay;
    [SerializeField]public int daysForWin;
    
    public Animator anim;
    public GameOverScreen GameOverScreen;
    public WinningScreen WinningScreen;
    private WorldStateManager WorldStateManager;
    
    //Cashed property index
    private static readonly int IsGameFinished = Animator.StringToHash("isGameFinished");

    protected override void Awake() {
        base.Awake();
        // Set start-health to 1000
        maxHealth = 1000;

        isLastDay = false;

        //Get animations
        anim = GetComponent<Animator>();
        
        //Get day number
        WorldStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldStateManager>();

    }


    /// <summary>
    /// Run every frame
    /// </summary>
    private void Update() {
        if (WorldStateManager.getCurrentIngameDay()-1 >= daysForWin) {
            isLastDay = true;
            OnLastDay();
        }
    }

    /// <summary>
    /// Spawns portal
    /// </summary>
    private void OnLastDay() {
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
    protected override void OnDestroy() {
        base.OnDestroy();
        GameOverScreen.Setup("The world-core was destroyed");
    }
    
}
