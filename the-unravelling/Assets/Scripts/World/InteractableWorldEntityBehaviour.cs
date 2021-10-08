using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class InteractableWorldEntityBehaviour : MonoBehaviour {
     
     // Scriptable object world entity interaction.
     public InteractableWorldEntity sharedData;
     public float health;

     
     
     public void Start() {
         health = sharedData.maxHealth;
     }
     
     
    // Called when player does damage to entity.
    public virtual void DamageOrDestroy() {
        if (health <= 0) {
            Debug.Log("Object destroyed, gave " + sharedData.GetResourceAmount());
            Destroy(gameObject);
        }
        else {
            health -= 20; //TODO get damage from player weapon 
        }
    }




}
