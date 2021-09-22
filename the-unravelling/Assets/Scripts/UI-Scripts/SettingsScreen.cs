using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.UI;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using TMPro;

public class SettingsScreen : MonoBehaviour {

    public Toggle fullscreen;
    public Toggle vSync;
    
    public List<ScreenDimensions> resolutions = new List<ScreenDimensions>();
    private int currentResIndex;

    public TMP_Text resolutionLabelText;

    // Start is called before the first frame update
    void Start() {
        fullscreen.isOn = Screen.fullScreen;
        vSync.isOn = QualitySettings.vSyncCount != 0;

        bool switchLabelToCurrentRes = false;
        for (int i = 0; i < resolutions.Count; i++) {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical) {
                switchLabelToCurrentRes = true;
                currentResIndex = i;
                UpdateResolutionLabel();
            }
        }

        if (!switchLabelToCurrentRes) {
            ScreenDimensions newScreenDimension = new ScreenDimensions();
            newScreenDimension.horizontal = Screen.width;
            newScreenDimension.vertical = Screen.height;
            
            // TODO(Elvis): The displayed resolution in the label is the smallest one, ideally that we show the largest one.
            resolutions.Insert(0,newScreenDimension);
            currentResIndex = resolutions.Count - 1;
            
            UpdateResolutionLabel();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolutionSwitchLeft() {
        currentResIndex--;
        if (currentResIndex < 0) {
            currentResIndex = 0;
        }

        UpdateResolutionLabel();
    }

    public void ResolutionSwitchRight() {
        currentResIndex++;
        if (currentResIndex > resolutions.Count - 1) {
            currentResIndex = resolutions.Count - 1; // put the resolution to 1-max
        }

        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel() {
        resolutionLabelText.text = resolutions[currentResIndex].horizontal.ToString() + " x " +
                                   resolutions[currentResIndex].vertical.ToString();
    }
    
    public void ApplyGraphicsChanges() {
        QualitySettings.vSyncCount = vSync.isOn ? 1 : 0;
        Screen.SetResolution(resolutions[currentResIndex].horizontal,resolutions[currentResIndex].vertical,fullscreen.isOn); // save 1 line of code by having fullscreen here!
    }
}
// TODO(Elvis): This did not work with an Vector2Int, why? Find out a more appropriate data structure than this class!
[System.Serializable]
public class ScreenDimensions {
    public int horizontal, vertical;
}