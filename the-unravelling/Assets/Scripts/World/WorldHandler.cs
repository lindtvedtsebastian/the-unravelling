
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class WorldHandler {
    public static void saveWorld(IWorld world) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/" + world.worldName + ".world");
        bf.Serialize(saveFile, world);
        saveFile.Close();

        // Take a screenshot of the players view
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + world.worldName + ".png");
    }

    public static IWorld loadWorld(string filename) {
        IWorld world = null;
        if (!File.Exists(Application.persistentDataPath + "/" + filename)) return world;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream loadFile = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
        world = (IWorld) bf.Deserialize(loadFile);
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
}