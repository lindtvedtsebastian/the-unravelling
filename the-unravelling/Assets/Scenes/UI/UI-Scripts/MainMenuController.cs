using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void exitButton() {
        Application.Quit();
        Debug.Log("Closed The Unraveling game");
    }

    public void startGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void generateMap() {
        MapGenerator.GenerateNoiseMap(64,1,50f,6,0.5f,2f,new Vector2(0,0));
    }

}
