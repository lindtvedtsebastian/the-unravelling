using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World core class
/// </summary>
public class WorldCore : MonoBehaviour, IClickable {
    [SerializeField]private int maxHealth;
    private int health;
    
    private bool isLastDay;
    private int daysForWin;
    
    public Animator anim;
    public GameObject GameOverScreen;
    public GameObject WinningScreen;
    
    private WorldManager _worldManager;
    private int[][] _entities;
    
  
    
    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;
    
    //Cashed property index
    private static readonly int IsGameFinished = Animator.StringToHash("isGameFinished");

     void Awake() {

         // Set start-health to 1000
        maxHealth = 1000;
        health = maxHealth;

        isLastDay = false;
        daysForWin = 10;

        //Get animations
        anim = GetComponent<Animator>();
        
        //Get objects on the ground.
        _worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        _entities = _worldManager.world.entities;
        
        //Initial spawn point
        var centerX = _worldManager.world.size / 2;
        var centerY = (_worldManager.world.size - 10) / 2;
        transform.position = StartPosition(centerX, centerY);
        
        
        //Spawn health bar 
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => HealthFraction;
        
        
    }

<<<<<<< HEAD
=======

   
>>>>>>> 684a0a26bddd3ba741082f5abd5504644eb6904f
    private void Update() {
        //If the number of days survived are the same or more than days required to win
        if (_worldManager.world.state.currentGameDay >= daysForWin) {
            isLastDay = true;
            OnLastDay();
        }
    }

    /// <summary>
    /// Spawns portal on last day.
    /// </summary>
    private void OnLastDay() {
        anim.SetBool(IsGameFinished, isLastDay);
    }

<<<<<<< HEAD
=======
    /// <summary>
    ///  If portal has spawned and the player collide with the portal
    ///  the game is won. 
    /// </summary>
    /// <param name="other">An objects collider</param>
>>>>>>> 684a0a26bddd3ba741082f5abd5504644eb6904f
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && isLastDay) {
            WinningScreen.GetComponent<WinningScreen>().Setup();
        }
    }

    /// <summary>
    /// Current fraction of max health. Range: [0, 1].
    /// </summary>
    public float HealthFraction => (float)health / (float)maxHealth;


    /// <summary>
    /// Subtracts damage from health and runs game over screen.
    /// </summary>
    /// <param name="damage">damage taken</param>
    /// <param name="damageFromNPC">Does the damage come from NPC</param>
    public void OnDamage(int damage, bool damageFromNPC) {
        health -= damage;
        if (health <= 0) {
            GameOverScreen.GetComponent<GameOverScreen>().Setup("The world-core was destroyed"); 
        }
    }

    /// <summary>
    /// Finds a position for the world core which is not blocked by another object.
    /// </summary>
    /// <returns>Vector2 x, y position</returns>
    private Vector2 StartPosition(int x, int y) {
        if (_entities[y][x] == 0) {
            return new Vector2(x, _entities.Length - y);
        }
        return StartPosition(x, y + 2);
    }
    
}
