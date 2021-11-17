using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverScreen : MonoBehaviour {
   
    public Text daysText;
        
    public void Setup(int daysSurvived) {
        gameObject.SetActive(true);
        daysText.text = "DAYS SURVIVED " + daysSurvived.ToString();
    }
}
