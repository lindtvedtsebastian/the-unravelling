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

    private int[] refreshRatesToAvoid = {50 ,56 ,59, 72, 75, 99, 119};
    
    // Start is called before the first frame update
    void Start() {
        resolutions = Screen.resolutions;

        List<string> chooseOptions = new List<string>();

        var resIndex = 0;

        for (var i = 0; i < resolutions.Length; i++) {
            //if (refreshRatesToAvoid.Contains(resolutions[i].refreshRate)) continue;
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

    public void SetFullscreen(bool fullscreenFlag) {
        Screen.fullScreen = fullscreenFlag;
    }

    public void SetResolution(int newResIndex) {
        Resolution res = resolutions[newResIndex];
        Screen.SetResolution(res.width,res.height,Screen.fullScreen);
    }
}