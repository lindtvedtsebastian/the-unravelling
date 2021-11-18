using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game over screen class
/// </summary>
public class GameOverScreen : MonoBehaviour {
   
    public Text daysText;
        
    /// <summary>
    /// Activates game over screen
    /// </summary>
    /// <param name="daysSurvived">number of days survived</param>
    /// <param name="text">reason for loosing</param>
    public void Setup(int daysSurvived, string text) {
        gameObject.SetActive(true);
        string looseText = text ?? "DAYS SURVIVED " + daysSurvived.ToString();
        daysText.text = looseText;
    }
}
