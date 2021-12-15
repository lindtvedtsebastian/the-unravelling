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
    [SerializeField] private TextAsset textAsset;

    private bool _inRange;

    private void Awake(){
        _inRange = false;
        visualCue.SetActive(false);
    }

    private void Update() {
        if (_inRange && !DialogueManager.instance.storyIsActive) {
            visualCue.SetActive(true);
        }
        else {
            visualCue.SetActive(false);  
        }
    }

    /// <summary>
    /// Checks if the player is inside collider 
    /// </summary>
    /// <param name="other">other gameobject's colliders</param>
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _inRange = true;
            DialogueManager.instance.SetCurrentStory(textAsset);
        }
    }

    /// <summary>
    /// Checks if the player has exited the collider 
    /// </summary>
    /// <param name="other">other gameobject's colliders</param>
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _inRange = false;
            DialogueManager.instance.ResetCurrentStory();
        }
    }
    
    
    
}
