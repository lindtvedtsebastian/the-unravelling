using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class LoadGameAddSave : MonoBehaviour  {
    private List<string> fileNames = new List<string>();
    private List<World> worlds = new List<World>();
    
    private DirectoryInfo dir;
    private FileInfo[] files;
    
    private string selectedWorld;
    
    public GameObject buttonPrefab;
    public GameObject loadGameButton;
    public GameObject ConfirmBox;
    public GameObject NoWorldSelectedBox;
    
    public RawImage previewImage;
    public TMP_Text TimeInfo;
    public TMP_Text GameDayInfo;
    public TMP_Text WorldDeleteText;

    public LevelLoader levelLoader;

    /// <summary>
    /// Start is ran when the gameobject is instantiated.
    /// In here we fetch all the world and we also fetch all the files for the filenames
    /// Using the filenames we dynamically populate the scrollview with button objects where we set the text
    /// to be the name of the file we retrieved
    /// We also add an listener for each of the buttons because we use that to fetch which file to delete.
    /// </summary>
    void Start() {
        worlds = WorldHandler.GetAllWorlds();
        dir = new DirectoryInfo(Application.persistentDataPath);
        files = dir.GetFiles("*.png");

        foreach (FileInfo file in files) {
            fileNames.Add(file.Name);
        }

        for (int i = 0; i < fileNames.Count; i++) {
            GameObject newButton = Instantiate(buttonPrefab);
            string fileName = fileNames[i];
            string worldName = fileNames[i].Replace(".png", "");
            newButton.transform.SetParent(transform,false);
            newButton.name = worldName;
            newButton.GetComponentInChildren<TMP_Text>().text = worldName;
            newButton.GetComponent<Button>().onClick.AddListener(() => SelectSave(fileName));
        }
    }

    /// <summary>
    /// SelectSave triggers the event for changing the preview image and sets
    /// the current selected button as savefile to load.
    /// </summary>
    public void SelectSave(string fileName) {
        loadGameButton.SetActive(true);
        selectedWorld = fileName.Replace(".png", "");
        var selectedWorldNoSuffix = fileName.Replace(".png", "");
        byte[] image = File.ReadAllBytes(Application.persistentDataPath + "/" + fileName);
        Texture2D tex = new Texture2D(1, 1); // Size does not matter, will be overwritten
        tex.LoadImage(image);
        previewImage.GetComponent<RawImage>().texture = tex;

        foreach (var worldInList in worlds.Where(worldInList => worldInList.worldName == selectedWorldNoSuffix)) {
            TimeInfo.text = worldInList.state.globalGameTime.ToString();
            GameDayInfo.text = worldInList.state.currentGameDay.ToString();
        }
        
    }

    /// <summary>
    /// Loads a selected game world if one is selected.
    /// If not it displays a ui box that clarifies this to the user.
    /// </summary>
    public void LoadGame() {
        if (!string.IsNullOrEmpty(selectedWorld)) {
            GameData.Get.activeWorld = selectedWorld;
            levelLoader.loadScene("MainGame");
        }
        else {
            NoWorldSelectedBox.SetActive(true);
        }
    }

    /// <summary>
    /// The UI element that is displayed when you press "delete" or "load game".
    /// This makes it so you can't accidentally delete a world a bit nicer than logging an error and crashing.
    /// </summary>
    public void DisplayConfirmBox() {
        if (!string.IsNullOrEmpty(selectedWorld)) {
            ConfirmBox.SetActive(true);
            WorldDeleteText.text = "Are you sure you want to delete\n\n" + selectedWorld;
        }
        else {
            NoWorldSelectedBox.SetActive(true);
        }
    }
    
    /// <summary>
    /// Deletes a selected game world by name.
    /// </summary>
    public void DeleteGameSave() {
        if (!string.IsNullOrEmpty(selectedWorld)) {
            var buttonThatIsPressedName = selectedWorld.Replace(".world", "");
            var buttonThatIsPressed = GameObject.Find(buttonThatIsPressedName);
            previewImage.GetComponent<RawImage>().texture = null;
            WorldHandler.DeleteWorld(selectedWorld);
            Destroy(buttonThatIsPressed);
            ConfirmBox.SetActive(false);
            selectedWorld = String.Empty;
        }
        else {
            Debug.LogError("No world save was selected!");
        }
    }
}
