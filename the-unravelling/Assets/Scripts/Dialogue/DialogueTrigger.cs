using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;


/// <summary>
/// Dialog trigger class for NPC handling story distribution and dialogue range
/// </summary>
public class DialogueTrigger : MonoBehaviour{
    
    [Header("Trigger UI")]
    [SerializeField] private GameObject visualCue;
    
    [Header("Text Assets")]
    [SerializeField] private TextAsset textAsset;

    public bool inRange;

    private void Awake(){
        inRange = false;
        visualCue.SetActive(false);
    }

    private void Update() {
        if (inRange && !DialogueManager.instance.storyIsActive) {
            visualCue.SetActive(true);
        }
        else {
            visualCue.SetActive(false);  
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inRange = true;
            DialogueManager.instance.SetCurrentStory(textAsset);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            inRange = false;
            DialogueManager.instance.ResetCurrentStory();
        }
    }
    
    
    
}
