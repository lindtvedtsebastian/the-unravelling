using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] _choicesText;

    public static DialogueManager instance { get; private set; }

    public bool storyIsActive { get; private set; }

    [SerializeField] private TextAsset defaultAsset;
    private Story _defaultStory;
    private Story _currentStory;

    private const string SpeakerTag = "speaker";

    private const string PortraitTag = "portrait";

    private const string LayoutTag = "layout";

    private void Awake() {
        
        // Protective check to avoid duplicate instances of a singleton object.
        if (instance != null) {
            Debug.LogWarning("The DialogueManager is a singleton object. Make sure there are no duplicate instances");
        }

        instance = this;
    }
    
    private void Start() {
        _defaultStory = new Story(defaultAsset.text);
        _currentStory = _defaultStory;
        
        storyIsActive = false;
        dialoguePanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        _choicesText = new TextMeshProUGUI[choices.Length];

        var index = 0;
        foreach (var choice in choices) {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void SetCurrentStory(TextAsset story) {
        _currentStory = new Story(story.text);
    }

    public void ResetCurrentStory() {
        _currentStory = _defaultStory;
    }

    public void EnterDialogueMode() {
        storyIsActive = true;
        dialoguePanel.SetActive(true);

        speakerName.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        ContinueStory();
    }

    private void DisplayChoices() {
        var currentChoices = _currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) {
            Debug.LogError("Unsupported number of choices. Number of choices given: "
                           + currentChoices.Count);
        }

        var index = 0;
        foreach (var choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
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
        _currentStory.ChooseChoiceIndex(choiceIndex);
    }

    private void HandleTags(List<string> currentTags) {
        foreach (var tag in currentTags) {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2) {
                Debug.LogError("Failed to parse tag: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey) {
                case SpeakerTag:
                    speakerName.text = tagValue;
                    break;
                case PortraitTag:
                    portraitAnimator.Play(tagValue);
                    break;
                case LayoutTag:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Incorrect handling of tag: " + tag);
                    break;
            }
        }
    }

    public void ContinueStory() {
        if (_currentStory.canContinue) {
            dialogueText.text = _currentStory.Continue();
           
            DisplayChoices();

            HandleTags(_currentStory.currentTags);
        }
        else {
            StartCoroutine(ExitDialogueMode());
        }
    }

    public IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.2f);
        
        storyIsActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    
    
}
