using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsScreen : MonoBehaviour {

    public Toggle fullscreen;
    public Toggle vSync;

    // Start is called before the first frame update
    void Start() {
        fullscreen.isOn = Screen.fullScreen;
        vSync.isOn = QualitySettings.vSyncCount != 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyGraphicsChanges() {
        Screen.fullScreen = fullscreen.isOn;
        QualitySettings.vSyncCount = vSync.isOn ? 1 : 0;
    }
}
