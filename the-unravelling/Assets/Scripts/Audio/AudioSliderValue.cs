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
    public TMP_Text sliderValue;
    public Toggle muteToggle;
    
    public float sizer;
    public string volumeParameter = "";
    
    private bool disableToggleEvent;
   
    private void Awake() {
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
        slider.value = mute ? 0.5f : 0.0001f;
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
    }

    /// <summary>
    /// Using this we update the text that is shown as to also give a visual
    /// indicator to the user of the ui as to what level the audio is at.
    /// </summary>
    /// <param name="value">Current value of the slider</param>
    public void UpdateText(float value) {
        sliderValue.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
