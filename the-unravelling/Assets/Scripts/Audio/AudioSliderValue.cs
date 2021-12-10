using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSliderValue : MonoBehaviour {
    public AudioMixer mixer;
    public Slider slider;
    public Toggle muteToggle;
    public GameObject audioMenu;
    
    public float sizer;
    public string volumeParameter = "MasterVolume";
    
    private bool disableToggleEvent;
   
    private void Awake() {
        // We are unable to modify values in structs returned from other components because of passing by value
        // So we need to modify the component itself then reassign it.
        //var temp = audioMenu.GetComponent<Image>().color;
        //temp.a = 1.0f;
        //audioMenu.GetComponent<Image>().color = temp;
        
        // Initially we turn of clicking on this GameObject but since we are now in this menu we need it back on.
        //audioMenu.GetComponent<Image>().raycastTarget = false;

        slider.onValueChanged.AddListener(SliderAudioChanged);
        muteToggle.onValueChanged.AddListener(SliderToggleChanged);
    }
        
    // Start is called before the first frame update
    void Start() {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
    }
    
    /// <summary>
    /// We need to save the volume that the user set
    /// This way we add our value to the key-value storage of the player preferences.
    /// </summary>
    private void OnDisable() {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }

    /// <summary>
    /// On enable of this toggle we mute the volume slider and otherwise we go back
    /// to the audio level that was previous to the mute state.
    /// </summary>
    /// <param name="mute">Are we muted or not?</param>
    private void SliderToggleChanged(bool mute) {
        if (disableToggleEvent) 
            return;
        slider.value = mute ? 1.0f : 0.0001f;
    }
    
    /// <summary>
    /// Modifies the value of the slider, which is connected to the mixer group
    /// in turn modifying the volume using the slider.
    /// </summary>
    /// <param name="val">Amount to change the volume to</param>
    private void SliderAudioChanged(float val) {
        mixer.SetFloat(volumeParameter, Mathf.Log10(val) * sizer);
        disableToggleEvent = true;
        muteToggle.isOn = slider.value > 0.0001f;
        disableToggleEvent = false;

        if (volumeParameter == "MasterVolume" && muteToggle.isOn) {
            
        }
    }
}
