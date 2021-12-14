using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumePercentageDisplay : MonoBehaviour {
    [SerializeField] TMP_Text sliderValue;
    
    /// <summary>
    /// Using this we update the text that is shown as to also give a visual
    /// indicator to the user of the ui as to what level the audio is at.
    /// </summary>
    /// <param name="value">Current value of the slider</param>
    public void UpdateText(float value) {
        sliderValue.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
