using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class SettingsScreen : MonoBehaviour {

    public Toggle fullscreen;
    public Toggle vSync;

    public TMP_Dropdown dropdown;
    private Resolution[] resolutions;
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
        var currentResolutionOnDropdown = dropdown.value;
        var res = resolutions[currentResolutionOnDropdown];
        Screen.SetResolution(res.width,res.height,fullscreen.isOn);
        
        QualitySettings.vSyncCount = vSync.isOn ? 1 : 0;
    }
}