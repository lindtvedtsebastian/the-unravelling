using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public InputField newWorldName;
    public InputField newWorldSeed;
    public Dropdown   worldSizeDropdown;
    private int worldSize;
    public WorldPreview worldPreview;
    private World world;


    void Start() {
        randomSeed();
        MapSizeChanged(worldSizeDropdown);
        worldSizeDropdown.onValueChanged.AddListener(delegate {
            MapSizeChanged(worldSizeDropdown);
        });
    }
    
    public void exitButton() {
        Application.Quit();
        Debug.Log("Closed The Unraveling game");
    }
    public void startGame() {
        if (world == null) {
            Debug.LogError("Invalid game data, did you generate a world?"); // Change to ingame message instead
            return;
        }
        WorldHandler.saveWorld(world);
        GameData.Get.activeWorld = world.worldName;
        SceneManager.LoadScene("MainGame");
    }

    public void generateMap() {
        int seed = int.Parse(newWorldSeed.text); // Know that content type is int 
        world = WorldGenerator.generateWorld(newWorldName.text, worldSize, seed);
        worldPreview.drawMap(world);
    }

    public void MapSizeChanged(Dropdown change) {
        switch (change.value) {
            case 0: worldSize = 256; break;
            case 1: worldSize = 512; break;
            case 2: worldSize = 1024; break;
        }
    }

    public void randomSeed() {
        int seed = new System.Random().Next(0, 999_999_999);
        newWorldSeed.text = seed.ToString();
    }
}
