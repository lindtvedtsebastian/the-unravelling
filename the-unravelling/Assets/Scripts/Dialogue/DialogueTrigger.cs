using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Dialog trigger class for NPCs
/// </summary>
public class DialogueTrigger : MonoBehaviour{
   
    //Player in range of NPC 
    private bool inRange;

    [SerializeField] private TextAsset json;


    [SerializeField] private GameObject notify;
    
    
    
    private void Awake(){
        inRange = false;
        notify.SetActive(false);
    }

    private void Update() {
        
         if(inRange){
             //notify.SetActive(true);
             Debug.Log(json.text);
         }
         else
         {
             //notify.SetActive(false);
         }
         
         
    }

    /// <summary>
    /// Checks if the player is inside collider 
    /// </summary>
    /// <param name="other">other gameobject's colliders</param>
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) 
            inRange = true;
    }

    /// <summary>
    /// Checks if the player has exited the collider 
    /// </summary>
    /// <param name="other">other gameobject's colliders</param>
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) 
            inRange = false;
    }
    
    
    
}
