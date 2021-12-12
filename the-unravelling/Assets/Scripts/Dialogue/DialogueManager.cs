using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class DialogueManager : MonoBehaviour {

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] private Button continueButton;

    public bool storyIsActive { get; private set; }

    public static DialogueManager instance { get; private set; }

    private void Awake() {

        if (instance != null) {
            Debug.LogWarning("The DialogueManager is a singleton object. Make sure there are no duplicate instances");
        }

        instance = this;
    }
    
    private void Start() {
        storyIsActive = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];

        var index = 0;
        foreach (var choice in choices) {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() {
        if (!storyIsActive) {
            return;
        }
        /*if () { // TODO("If player presses the interact button.")
            ContinueStory();
        }*/
    }

    public void EnterDialogueMode(TextAsset story) {
        currentStory = new Story(story.text);
        storyIsActive = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void DisplayChoices() {
        var currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) {
            Debug.LogError("Unsupported number of choices. Number of choices given: "
                           + currentChoices.Count);
        }

        var index = 0;
        foreach (var choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++) {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(ChoiceProgression());
    }

    private IEnumerator ChoiceProgression() {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void Choose(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }

    public void ContinueStory() {
        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.2f);
        
        storyIsActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    
    
}
