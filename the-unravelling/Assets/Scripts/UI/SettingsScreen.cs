using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine.UI;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using TMPro;
using UnityEngine.InputSystem.OnScreen;

public class SettingsScreen : MonoBehaviour {

    public Toggle fullscreen;
    public Toggle vSync;

    public TMP_Dropdown dropdown;
    private Resolution[] resolutions;
    // Start is called before the first frame update
    void Start() {
        resolutions = Screen.resolutions; // this varies greatly, on my system it's about 24. This is given to the operating system by the monitor directly
        List<string> chooseOptions = new List<string>();
        var resIndex = 0;
        
        var highestResolution = resolutions.Last();
        var test = resolutions.Length-1;
        var lastres = resolutions[test];
        Debug.Log(lastres.width + " " + lastres.height + " " + lastres.refreshRate);
        
        
        var highestRefreshRate = highestResolution.refreshRate;

        // TODO: Make this one work.
        /*for (var i = 0; i < resolutions.Length; i++) {
            if (resolutions[i].refreshRate == highestRefreshRate) {
                var oneResolutionOption = resolutions[i].width + " x " + resolutions[i].height + " " +
                                          resolutions[i].refreshRate + "hz";
                chooseOptions.Add(oneResolutionOption);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
                    resIndex = i;
                }
            }
        }*/
        
        // TODO: Why does this work perfectly expect having to many refresh-rates but the one above builds completely wrong
        for (var i = 0; i < resolutions.Length; i++) {
            var oneResolutionOption = resolutions[i].width + " x " + resolutions[i].height + " " +
                                      resolutions[i].refreshRate + "hz";
            chooseOptions.Add(oneResolutionOption);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) { 
                resIndex = i;
            }
        }
        dropdown.AddOptions(chooseOptions);
        dropdown.value = resIndex;
        dropdown.RefreshShownValue();
    }

    public void SetFullscreen(bool fullscreenFlag) {
        Screen.fullScreen = fullscreenFlag;
    }

    public void SetResolution(int newResIndex) {
        var res = resolutions[newResIndex];
        Screen.SetResolution(res.width,res.height,Screen.fullScreen);
    }
}