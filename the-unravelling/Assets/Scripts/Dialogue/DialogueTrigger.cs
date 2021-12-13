using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;


/// <summary>
/// Dialog trigger class for NPCs
/// </summary>
public class DialogueTrigger : MonoBehaviour{
    
    [Header("Trigger UI")]
    [SerializeField] private GameObject visualCue;
    
    [Header("Text Assets")]
    [SerializeField] private TextAsset[] textAssets;

    private bool _inRange;

    private void Awake(){
        _inRange = false;
        visualCue.SetActive(false);
    }

    private void Update() {
        
        if (_inRange && !DialogueManager.instance.storyIsActive) {
             visualCue.SetActive(true);
             
             if (true) { // TODO (If player presses the interact button)
                 DialogueManager.instance.EnterDialogueMode(textAssets[0]); // TODO (StoryProgression)
             }
        }
        else {
          visualCue.SetActive(false);  
        } 
    }

    private TextAsset StoryProgression() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Checks if the player is inside collider 
    /// </summary>
    /// <param name="other">other gameobject's colliders</param>
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) 
            _inRange = true;
    }

    /// <summary>
    /// Checks if the player has exited the collider 
    /// </summary>
    /// <param name="other">other gameobject's colliders</param>
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) 
            _inRange = false;
    }
    
    
    
}
