using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Winning screen class
/// </summary>
public partial class WinningScreen : MonoBehaviour {
    
     
    public void Setup() {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Handles click on continue to infinity button
    /// </summary>
    public void ContinueButton() {
        gameObject.SetActive(false);
    }
    
    
    /// <summary>
    /// Saves game state and loads main menu scene
    /// </summary>
    public void MainMenuButton() {
        GameData.Get.SaveWorld();
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    
    
    
}
