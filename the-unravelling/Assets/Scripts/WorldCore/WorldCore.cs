using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World core class
/// </summary>
public class WorldCore : BaseUnit {
    
    [SerializeField]public bool isLastDay = false;
    
    public Collider2D collider;
    public Animator anim;
    
  
    void Awake() {
        // Set start-health to 100
        maxHealth = 100;

        //Get collider
        collider = GetComponent<Collider2D>();
        
        //Get animations
        anim = GetComponent<Animator>();
    }


    private void Update() {
        onLastDay();
    }

    /// <summary>
    /// Spawns portal
    /// </summary>
    public void onLastDay() {
        anim.SetBool("isGameFinished", isLastDay);
    }

    
    
    






}
