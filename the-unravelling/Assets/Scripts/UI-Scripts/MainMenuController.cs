using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public InputField newWorldName;
    // Start is called before the first frame update
    public void exitButton() {
        Application.Quit();
        Debug.Log("Closed The Unraveling game");
    }

    public void startGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void generateMap() {
        MapGenerator.GenerateNoiseMap(newWorldName.text,64,1,50f,6,0.5f,2f,new Vector2(0,0));
        newWorldName.text = "Enter game world name";
    }

}
