using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public InputField newWorldName;
    public InputField newWorldSeed;
    public MapPreview mapPreview;
    // Start is called before the first frame update
    public void exitButton() {
        Application.Quit();
        Debug.Log("Closed The Unraveling game");
    }

    public void startGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void generateMap() {
        int seed = newWorldSeed.text.GetHashCode();
        if (newWorldSeed.text == "")
            seed = new System.Random().Next(0,1_000_000);
        MapGenerator.GenerateNoiseMap(newWorldName.text,64,seed,50f,6,0.5f,2f,new Vector2(0,0));
        mapPreview.drawMap();
        newWorldName.text = "Enter game world name";
    }

}
