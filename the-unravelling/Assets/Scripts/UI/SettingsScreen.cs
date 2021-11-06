using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Toggle = UnityEngine.UI.Toggle;
public class SettingsScreen : MonoBehaviour {

    public GameObject RevertChangesBox;
    public Toggle vSync;
    public TMP_Text timerText;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown displayModeDropdown;
    
    private Resolution[] resolutions;
    private Resolution oldResolutionState;
    private FullScreenMode oldDisplayMode;
    private bool oldFullscreenState;
    private int oldVsyncState;
    private int oldResIndexOnDropdown;
    List<string> displayModeOptions = new List<string> { "Exclusive FullScreen", "FullScreen Window", "Windowed"};
    
    // Start is called before the first frame update
    void Start() {
        // fill the resolution dropdown with all resolutions.
        resolutions = Screen.resolutions; // this varies greatly, on my system it's about 24. This is given to the operating system by the monitor directly
        var chooseOptions = new List<string>();
        var resIndex = 0;
        
        Array.Reverse(resolutions,0,resolutions.Length); // put what is determined to be the largest at the top of the list.
        
        for (var i = 0; i < resolutions.Length; i++) {
            var oneResolutionOption = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            chooseOptions.Add(oneResolutionOption);
                
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) { 
                resIndex = i; // Make the value that is default shown on the dropdown is the current one of the screen.
            }
        }
        resolutionDropdown.AddOptions(chooseOptions);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();
        
        // Add the display mode options. We don't support maximized windowed because it's unstable on various platforms.
        displayModeDropdown.AddOptions(displayModeOptions);
    }
    
    public void ApplyGraphicsChanges() {
        // We store the state of this data as it was in the moment you pressed the apply changes button so we can revert to it.
        oldResolutionState = new Resolution {
            width = Screen.currentResolution.width,
            height = Screen.currentResolution.height,
            refreshRate = Screen.currentResolution.refreshRate
        };
        oldVsyncState = QualitySettings.vSyncCount;
        oldResIndexOnDropdown = resolutionDropdown.value;
        oldDisplayMode = Screen.fullScreenMode;

        var currentResolutionOnDropdown = resolutionDropdown.value;
        var currentDisplayModeSelection =
            GetDisplayModeFromInt(displayModeDropdown.options[displayModeDropdown.value].text);
        var res = resolutions[currentResolutionOnDropdown];
        
        Screen.SetResolution(res.width,res.height,currentDisplayModeSelection);
        QualitySettings.vSyncCount = vSync.isOn ? 1 : 0;

        StartCoroutine(Countdown());
    }
    /// <summary>
    /// To be able to keep the changes made by the user we run this function to stop the
    /// co-routine which would revert the changes if kept running until the timer reaches
    /// 0 in which case it would run the revertChanges() function.
    /// </summary>
    public void KeepChanges() {
        StopAllCoroutines();
    }

    /// <summary>
    /// When the timer runs out in the co-routine (counter reaches 0) we set the state of the graphics to the previous state
    /// before the changes were applied.
    /// </summary>
    public void RevertChanges() {
        StopAllCoroutines();
        
        Screen.SetResolution(oldResolutionState.width,oldResolutionState.height,oldDisplayMode);
        QualitySettings.vSyncCount = oldVsyncState;
        RevertChangesBox.SetActive(false);
        resolutionDropdown.value = oldResIndexOnDropdown;
    }
 
    /// <summary>
    /// Timer function that is used mainly for displaying a number on the UI but also to trigger as to when we should
    /// revert to the previous graphics state.
    /// </summary>
    /// <returns>coruoutine </returns>
    IEnumerator Countdown () {
        var counter = 10;
        while (counter > 0) {
            timerText.text = counter.ToString();
            yield return new WaitForSeconds (1);
            counter--;
        }
        timerText.text = "";
        RevertChanges();
    }

    /// <summary>
    /// Utility function to get the correct enum, we need to do this because we
    /// do not support MaximizedWindow because of it's instability.
    /// It is the second to last in the list of enum and because of that interferes with the indices in the dropdown menu
    /// so we go by name instead.
    /// </summary>
    /// <param name="name">Name of the selection in the dropdown menu</param>
    /// <returns>Display mode of the screen to apply</returns>
    private FullScreenMode GetDisplayModeFromInt(string name) {
        return name switch {
            "Exclusive FullScreen" => FullScreenMode.ExclusiveFullScreen,
            "FullScreen Window" => FullScreenMode.FullScreenWindow,
            "Windowed" => FullScreenMode.Windowed,
            _ => FullScreenMode.FullScreenWindow
        };
    }
}