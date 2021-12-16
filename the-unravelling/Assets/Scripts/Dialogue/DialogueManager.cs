using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// DialogueManager class handling all logic and UI for dialogue
/// </summary>
public class DialogueManager : MonoBehaviour {
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Animator portraitAnimator;

    [SerializeField] private InputController _inputController;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] _choicesText;

    [Header("Default dialogue")]
    [SerializeField] private TextAsset defaultAsset;
    private Story _defaultStory;
    private Story _currentStory;
    
    public static DialogueManager instance { get; private set; }
    
    public bool storyIsActive { get; private set; }

    // Tags from the Ink text asset
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

    /// <summary>
    /// Method for setting dialogue from an NPC
    /// </summary>
    /// <param name="story">NPC dialogue json data</param>
    public void SetCurrentStory(TextAsset story) {
        _currentStory = new Story(story.text);
    }
    
    /// <summary>
    /// Method for resetting to default 
    /// </summary>
    public void ResetCurrentStory() {
        _currentStory = _defaultStory;
    }

    /// <summary>
    /// Method for entering dialogue mode
    /// </summary>
    public void EnterDialogueMode() {
        storyIsActive = true;
        dialoguePanel.SetActive(true);

        speakerName.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        StartCoroutine(ContinueStory());
    }
    
    /// <summary>
    /// Method for displaying up to 6 choices during the given dialogue point in the story
    /// </summary>
    private void DisplayChoices() {
        var currentChoices = _currentStory.currentChoices;

        // Defensive check for not exceeding the UI limitation of six choices
        if (currentChoices.Count > choices.Length) {
            Debug.LogError("Unsupported number of choices. Number of choices given: "
                           + currentChoices.Count);
        }
        
        // Looping through choices and activating corresponding UI
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
    
    /// <summary>
    /// Method for updating players choice from UI
    /// </summary>
    /// <returns>Run as a coroutine to handle delay in UI graphics</returns>
    private IEnumerator ChoiceProgression() {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    
    /// <summary>
    /// Method for passing the UI choice index to the Ink Story class
    /// </summary>
    /// <param name="choiceIndex">Index of the given choice</param>
    public void Choose(int choiceIndex) {
        _currentStory.ChooseChoiceIndex(choiceIndex);
    }

    /// <summary>
    /// Method for handling tags in Ink story data
    /// </summary>
    /// <param name="currentTags"></param>
    private void HandleTags(List<string> currentTags) {
        
        // Tags are written as (key, value) pairs so they must be split
        foreach (var tag in currentTags) {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2) {
                Debug.LogError("Failed to parse tag: " + tag);
            }

            // Removing whitespace
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Parsing tags based on key
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

    /// <summary>
    /// Method for handling updating UI based on the current point in dialogue
    /// </summary>
    /// <returns>Run as a coroutine to handle input delay</returns>
    public IEnumerator ContinueStory() {
        yield return new WaitForSeconds(0.2f);
        
        // Defensive check for equivalent of end of file
        if (_currentStory.canContinue) {
            dialogueText.text = _currentStory.Continue();
           
            DisplayChoices();

            HandleTags(_currentStory.currentTags);
        }
        else {
            StartCoroutine(ExitDialogueMode());
        }
    }

    /// <summary>
    /// Method for exiting dialogue mode
    /// </summary>
    /// <returns>Run as a coroutine to handle UI graphics delay</returns>
    public IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.2f);
        
        storyIsActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
