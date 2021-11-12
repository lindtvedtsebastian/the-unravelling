using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSliderValue : MonoBehaviour {
    private TMP_Text sliderValue;

    [SerializeField] 
    string volumeParameter = "MasterVolume";
    [SerializeField] 
    AudioMixer mixer;
    [SerializeField] 
    Slider slider;
    [SerializeField]  
    float sizer;
    [SerializeField]
    Toggle masterVolumeToggle;

    private bool disableToggleEvent;

    private void Awake() {
        slider.onValueChanged.AddListener(SliderAudioChanged);
        masterVolumeToggle.onValueChanged.AddListener(SliderToggleChanged);
    }

    private void SliderToggleChanged(bool mute) {
        if (disableToggleEvent) 
            return;
        slider.value = mute ? 0.5f : 0.0001f;
    }
    
    private void SliderAudioChanged(float val) {
        mixer.SetFloat(volumeParameter, Mathf.Log10(val) * sizer);
        disableToggleEvent = true;
        masterVolumeToggle.isOn = slider.value > 0.0001f;
        disableToggleEvent = false;
    }

    private void OnDisable() {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }
    // Start is called before the first frame update
    void Start() {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);
        sliderValue = GetComponent<TMP_Text>();
    }

    /*public void UpdateText(float value) {
        sliderValue.text = Mathf.RoundToInt(value * 100) + "%";
    }*/
}
