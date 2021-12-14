using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World core class
/// </summary>
public class WorldCore : MonoBehaviour, IClickable {
    [SerializeField]private int maxHealth; 
    //Current health
    private int health;
    
    private bool isLastDay;
    [SerializeField]public int daysForWin;
    
    public Animator anim;
    public GameOverScreen GameOverScreen;
    public WinningScreen WinningScreen;
    private WorldManager _worldManager;
    
    
    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;
    
    //Cashed property index
    private static readonly int IsGameFinished = Animator.StringToHash("isGameFinished");

     void Awake() {
        
        // Set start-health to 1000
        maxHealth = 1000;
        health = maxHealth;

        isLastDay = false;

        //Get animations
        anim = GetComponent<Animator>();
        
        //Get day number
        _worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        
        //Spawn health bar 
        // Spawn health bar
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => HealthFraction;

    }


    /// <summary>
    /// Run every frame
    /// </summary>
    private void Update() {
        if (_worldManager.world.state.currentGameDay >= daysForWin) {
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
    /// Current fraction of max health. Range: [0, 1].
    /// </summary>
    public float HealthFraction => (float)health / (float)maxHealth;

    
    /// <summary>
    /// When destroyed displays game over screen.
    /// </summary>
    void OnDestroy() {
        GameOverScreen.Setup("The world-core was destroyed");
    }

    public void OnDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
