using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Data")]
public class GameData : ScriptableObjectSingleton<GameData> {
    public World world;

    public const int NW = 0b1;
    public const int N  = 0b10;
    public const int NE = 0b100;
    public const int W  = 0b1000;
    public const int E  = 0b10000;
    public const int SW = 0b100000;
    public const int S  = 0b1000000;
    public const int SE = 0b10000000;

    public ItemData[] worldEntities;
    public TileBase[] FOG;

    /// <summary>
    /// Constructor for the GameData class 
    /// </summary>
    GameData() {
        world = new World();
    }

    /// <summary>
    /// Takes a screenshot of the current player view, then saves the world
    /// </summary>
    public void SaveWorld() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/" + world.mapName + ".world"); 
        bf.Serialize(saveFile,world);
        saveFile.Close();

        // Take a screenshot of the players view
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + world.mapName + ".png");
    }

    /// <summary>
    /// Loads the world file at the filename location in the persistent data path
    /// </summary>
    /// <param name="filename"></param>
    public void LoadWorld(string filename = "game-world") {
        if (File.Exists(Application.persistentDataPath + "/" + filename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream loadFile = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            world = (World) bf.Deserialize(loadFile);
        }
        else world = new World();
    }
    public List<World> GetAllWorlds() {
        List<World> returnList = new List<World>();
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.world");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream loadFile;
        foreach (var file in files) {
            loadFile = File.Open(file , FileMode.Open);
            world = (World) bf.Deserialize(loadFile);
            returnList.Add(world);
        }
        return returnList;
    }
    
    /// <summary>
    /// Deletes a single file located at application persistent storage path.
    /// </summary>
    /// <param name="filename">Name of the file we are attempting to delete</param>
    public void DeleteWorld(string filename = "game-world") {
        if (File.Exists(Application.persistentDataPath + "/" + filename)) {
            File.Delete(Application.persistentDataPath + "/" + filename);
            
            var pngDelete = filename.Replace(".world", ".png");
            File.Delete(Application.persistentDataPath + "/" + pngDelete);
        } else {
            Debug.LogError("Could not locate file" + Application.persistentDataPath + "/" + filename);
        }
    }
}

[Serializable]
public class World {
    public WorldState state;
    public int worldSize;
    public int[,] map;
    public int[,] background;
    public int[,] pathfindingMap;
    public List<IEntity> iEntities;
    public string mapName;

    public World() {
        state = new WorldState();
        iEntities = new List<IEntity>();
    }
}

[Serializable]
public class IEntity {
    public float worldPosX;
    public float worldPosY;
    public int entityID;


    public IEntity(float x, float y, int entityID) {
        worldPosX = x;
        worldPosY = y;
        this.entityID = entityID;
    }
}
