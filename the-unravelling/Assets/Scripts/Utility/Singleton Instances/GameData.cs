using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Data")]
public class GameData : ScriptableObjectSingleton<GameData> {
    public string activeWorld;

    public ItemData[] worldEntities;
    public TileBase[] FOG;

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
