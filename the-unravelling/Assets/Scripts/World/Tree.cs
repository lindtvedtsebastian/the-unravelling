
using System;
using UnityEngine;
using Random = System.Random;

public class Tree : InteractableWorldEntityBehaviour {
    
    
    
    
    //Run when game-object is destroyed
    private void OnDestroy() {
        Debug.Log(health);
        Debug.Log("Tree object destroyed, gave " + sharedData.GetResourceAmount() + " wood!" );   
       
    }

    public void DamageOrDestroy() {
        if (health <= 0) {
            Debug.Log("Tree object destroyed, gave " + sharedData.GetResourceAmount() + " wood!" );
            Destroy(this);
        }
        else {
            health -= 20; //TODO get damage from player weapon 
        }
    }
    
    

    
}
