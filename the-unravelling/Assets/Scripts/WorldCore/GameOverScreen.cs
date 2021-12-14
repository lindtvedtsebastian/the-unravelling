using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Game over screen class
/// </summary>
public class GameOverScreen : MonoBehaviour {
   
    public Text daysText;
        
    /// <summary>
    /// Sets game over screen active.
    /// </summary>
    /// <param name="text">Reason for loosing</param>
    /// <param name="daysSurvived">Number of days survived</param>
    public void Setup(string text = null, int daysSurvived = 0) {
        gameObject.SetActive(true);
        string looseText = text ?? "DAYS SURVIVED: " + daysSurvived.ToString();
        daysText.text = looseText;
    }

    /// <summary>
    /// Loads main menu scene
    /// </summary>
    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    
}
