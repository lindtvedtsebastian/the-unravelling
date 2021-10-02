using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public InputField newWorldName;
    public InputField newWorldSeed;
    public Dropdown   mapSize;
    public MapPreview mapPreview;


    void Start() {
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
            Debug.LogError("Invalid game data, did you press generate?"); // NOTE: This should most likely pop up/active an little ui window instead.
            return;
        }
        SceneManager.LoadScene("MainGame");
    }

    public void generateMap() {
        int seed = newWorldSeed.text.GetHashCode();
        if (newWorldSeed.text == "")
            seed = new System.Random().Next(0,1_000_000);
        MapGenerator.GenerateTilemap(newWorldName.text,GameData.Get.world.worldSize,seed,50f,6,0.5f,2f,new Vector2(0,0));
        mapPreview.drawMap();
        newWorldName.text = "";
    }

    public void MapSizeChanged(Dropdown change) {
        switch (change.value) {
            case 0: GameData.Get.world.worldSize = 256; break;
            case 1: GameData.Get.world.worldSize = 512; break;
            case 2: GameData.Get.world.worldSize = 1024; break;
        }
    }
}
