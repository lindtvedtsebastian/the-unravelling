using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Game Data", menuName = "Scriptable Objects/World/Game Data")]
public class GameData : ScriptableObjectSingleton<GameData> {
    public Manager gameManager;
    
    public World world;

    public const int NW = 0b1;
    public const int N  = 0b10;
    public const int NE = 0b100;
    public const int W  = 0b1000;
    public const int E  = 0b10000;
    public const int SW = 0b100000;
    public const int S  = 0b1000000;
    public const int SE = 0b10000000;

    public BitmaskableWorldEntity GRASS;
	public BitmaskableWorldEntity DIRT;
	public WorldEntity STONE;
    public TileBase[] FOG;

    GameData() {
        world = new World();
    }
    
    public void SaveWorld(string filename = "game-world") {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/" + world.mapName + ".world"); 
        bf.Serialize(saveFile,world);
        saveFile.Close();

        // Take a screenshot of the players view
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + world.mapName + ".png");
    }

    public void LoadWorld(string filename = "game-world") {
        if (File.Exists(Application.persistentDataPath + "/" + filename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream loadFile = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            world = (World) bf.Deserialize(loadFile);
        }
        else world = new World();
    }
}

[Serializable]
public class World {
    public int worldSize;
    public int[,] map;
    public int[,] background;
    public string mapName;
    public float gameTime;
    public int day;
}
