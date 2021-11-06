using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;


public class SettingsScreen : MonoBehaviour {

    public GameObject RevertChangesBox;
    public Button ConfirmChanges;

    public Toggle fullscreen;
    public Toggle vSync;

    public TMP_Text timerText;

    public TMP_Dropdown dropdown;
    private Resolution[] resolutions;

    private bool oldFullscreenState;
    private int oldVsyncState;
    private Resolution oldResolutionState;
    private int oldResIndexOnDropdown;
    
    // Start is called before the first frame update
    void Start() {
        resolutions = Screen.resolutions; // this varies greatly, on my system it's about 24. This is given to the operating system by the monitor directly
        var chooseOptions = new List<string>();
        var resIndex = 0;
        
        Array.Reverse(resolutions,0,resolutions.Length);
        
        for (var i = 0; i < resolutions.Length; i++) {
            var oneResolutionOption = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            chooseOptions.Add(oneResolutionOption);
                
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) { 
                resIndex = i;
            }
        }
        dropdown.AddOptions(chooseOptions);
        dropdown.value = resIndex;
        dropdown.RefreshShownValue();
    }

    public void ApplyGraphicsChanges() {
        oldResolutionState = new Resolution {
            width = Screen.currentResolution.width,
            height = Screen.currentResolution.height,
            refreshRate = Screen.currentResolution.refreshRate
        };
        oldVsyncState = QualitySettings.vSyncCount;

        var currentResolutionOnDropdown = dropdown.value;
        oldResIndexOnDropdown = dropdown.value;
        var res = resolutions[currentResolutionOnDropdown];
        Screen.SetResolution(res.width,res.height,fullscreen.isOn);
        
        QualitySettings.vSyncCount = vSync.isOn ? 1 : 0;

        StartCoroutine(Countdown());
    }

    public void KeepChanges() {
        StopAllCoroutines();
    }
    
    public void RevertChanges() {
        StopAllCoroutines();
        
        Screen.SetResolution(oldResolutionState.width,oldResolutionState.height,fullscreen.isOn);
        QualitySettings.vSyncCount = oldVsyncState;
        RevertChangesBox.SetActive(false);
        dropdown.value = oldResIndexOnDropdown;
    }
 
    IEnumerator Countdown () {
        int counter = 10;
        while (counter > 0) {
            timerText.text = counter.ToString();
            yield return new WaitForSeconds (1);
            counter--;
        }
        timerText.text = "";
        RevertChanges();
    }
}