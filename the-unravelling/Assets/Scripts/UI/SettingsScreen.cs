using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Toggle = UnityEngine.UI.Toggle;


public class SettingsScreen : MonoBehaviour {

    public GameObject RevertChangesBox;

    public Toggle fullscreen;
    public Toggle vSync;

    public TMP_Text timerText;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown displayModeDropdown;
    private Resolution[] resolutions;
    List<string> displayModeOptions = new List<string> { "Exclusive FullScreen", "FullScreen Window", "Windowed"};

    private bool oldFullscreenState;
    private int oldVsyncState;
    private Resolution oldResolutionState;
    private int oldResIndexOnDropdown;
    private FullScreenMode oldDisplayMode;
    
    // Start is called before the first frame update
    void Start() {
        // Exclusive FullScreen 0 
        // FullScreenWindow	1
        // Windowed 3


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

    public void KeepChanges() {
        StopAllCoroutines();
    }

    public void RevertChanges() {
        StopAllCoroutines();
        
        Screen.SetResolution(oldResolutionState.width,oldResolutionState.height,oldDisplayMode);
        QualitySettings.vSyncCount = oldVsyncState;
        RevertChangesBox.SetActive(false);
        resolutionDropdown.value = oldResIndexOnDropdown;
    }
 
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

    private FullScreenMode GetDisplayModeFromInt(string name) {
        return name switch {
            "Exclusive FullScreen" => FullScreenMode.ExclusiveFullScreen,
            "FullScreen Window" => FullScreenMode.FullScreenWindow,
            "Windowed" => FullScreenMode.Windowed,
            _ => FullScreenMode.FullScreenWindow
        };
    }
}