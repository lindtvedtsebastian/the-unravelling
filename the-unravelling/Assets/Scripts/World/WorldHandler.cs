
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class WorldHandler {
    public static void saveWorld(World world) {
        Debug.Log(world.worldName);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/" + world.worldName + ".world");
        bf.Serialize(saveFile, world);
        saveFile.Close();

        // Take a screenshot of the players view
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + world.worldName + ".png");
    }

    public static World loadWorld(string filename) {
        World world = null;
        var filepath = Application.persistentDataPath + "/" + filename + ".world";
        if (!File.Exists(filepath)) return world;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream loadFile = File.Open(filepath, FileMode.Open);
        world = (World) bf.Deserialize(loadFile);
        loadFile.Close();
        return world;
    }

    /// <summary>
    /// Deletes a single file located at application persistent storage path.
    /// </summary>
    /// <param name="filename">Name of the file we are attempting to delete</param>
    public static void DeleteWorld(string filename = "game-world") {
        if (File.Exists(Application.persistentDataPath + "/" + filename)) {
            File.Delete(Application.persistentDataPath + "/" + filename);

            var pngDelete = filename.Replace(".world", ".png");
            File.Delete(Application.persistentDataPath + "/" + pngDelete);
        }
        else {
            Debug.LogError("Could not locate file" + Application.persistentDataPath + "/" + filename);
        }
    }

    public static List<World> GetAllWorlds() {
        List<World> returnList = new List<World>();
        string[] files = Directory.GetFiles(Application.persistentDataPath, "*.world");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream loadFile;
        foreach (var file in files) {
            loadFile = File.Open(file, FileMode.Open);
            World world = (World) bf.Deserialize(loadFile);
            returnList.Add(world);
        }

        return returnList;
    }
}