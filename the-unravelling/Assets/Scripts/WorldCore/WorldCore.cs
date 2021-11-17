using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  World core class
/// </summary>
public class WorldCore : BaseUnit { //TODO: handle destruction of gameobject.
    
    [SerializeField]public bool isLastDay = false;
    
    public Animator anim;
    
  
    void Awake() {
        // Set start-health to 100
        maxHealth = 100;
        
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
        anim.SetBool("isGameFinished", isLastDay);
    }

    /// <summary>
    ///  If portal has spawned and the player touches the portal
    ///  the game is won. 
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player") && isLastDay) {
            //end game 
        }
    }

    
    
    /*  
    private void onDestroy()
    {
        // display game over screen 
    }*/
    
}
