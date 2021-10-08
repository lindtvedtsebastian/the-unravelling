using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class InteractableWorldEntityBehaviour : MonoBehaviour {
     
     // Scriptable object world entity interaction.
     public InteractableWorldEntity sharedData;
     public float health;

     public InteractableWorldEntityBehaviour() {
         health = sharedData.maxHealth;
     }
     
     
    // Called when player does damage to entity.
    public virtual void DamageOrDestroy() {
        if (sharedData.maxHealth <= 0) {
            Debug.Log("Tree object destroyed, gave " + sharedData.GetResourceAmount() + " wood!" );
            Destroy(this);
        }
        else {
            sharedData.maxHealth -= 20; //TODO get damage from player weapon 
        }
    }




}
