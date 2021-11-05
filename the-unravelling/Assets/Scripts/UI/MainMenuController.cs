using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public InputField newWorldName;
    public InputField newWorldSeed;
    public Dropdown   mapSize;
    public MapPreview mapPreview;


    void Start() {
        randomSeed();
        MapSizeChanged(mapSize);
        mapSize.onValueChanged.AddListener(delegate {
            MapSizeChanged(mapSize);
        });
    }
    
    public void exitButton() {
        Application.Quit();
        Debug.Log("Closed The Unraveling game");
    }
    public void startGame() {
        if (GameData.Get.world.map == null) {
            Debug.LogError("Invalid game data, did you press generate?"); // Change to ingame message instead
            return;
        }
        SceneManager.LoadScene("MainGame");
    }

    public void generateMap() {
        int seed = int.Parse(newWorldSeed.text); // Know that content type is int 
        MapGenerator.GenerateTilemap(newWorldName.text,GameData.Get.world.worldSize,seed,50f,6,0.5f,2f,new Vector2(0,0));
        mapPreview.drawMap();
    }

    public void MapSizeChanged(Dropdown change) {
        switch (change.value) {
            case 0: GameData.Get.world.worldSize = 256; break;
            case 1: GameData.Get.world.worldSize = 512; break;
            case 2: GameData.Get.world.worldSize = 1024; break;
        }
    }

    public void randomSeed() {
        int seed = new System.Random().Next(0, 999_999_999);
        newWorldSeed.text = seed.ToString();
    }
}
