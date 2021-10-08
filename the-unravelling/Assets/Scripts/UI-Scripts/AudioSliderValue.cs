using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioSliderValue : MonoBehaviour {
    private TMP_Text sliderValue; 


    // Start is called before the first frame update
    void Start() {
        sliderValue = GetComponent<TMP_Text>();
    }

    public void UpdateText(float value) {
        sliderValue.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
