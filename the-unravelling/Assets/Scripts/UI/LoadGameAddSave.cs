using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadGameAddSave : MonoBehaviour, IPointerClickHandler
{
    private List<string> fileNames = new List<string>();
    public List<int> GameSaveButtonIndices = new List<int>();
    private int buttonIndex = 0;
    private string selectedWorld;
    private DirectoryInfo dir;
    private FileInfo[] files;
    public GameObject buttonPrefab;
    public GameObject loadGameButton;
    public RawImage previewImage;
    public GameObject deleteButton;


    void Start() {
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
            buttonIndex++;
            GameSaveButtonIndices.Add(buttonIndex);
        }
    }

    /// <summary>
    /// SelectSave triggers the event for changing the preview image and sets
    /// the current selected button as savefile to load.
    /// </summary>
    public void SelectSave(string fileName) {
        loadGameButton.SetActive(true);
        selectedWorld = fileName.Replace(".png", ".world");
        byte[] image = File.ReadAllBytes(Application.persistentDataPath + "/" + fileName);
        Texture2D tex = new Texture2D(1, 1); // Size does not matter, will be overwritten
        tex.LoadImage(image);
        previewImage.GetComponent<RawImage>().texture = tex;
    }

    public void LoadGame() {
        GameData.Get.LoadWorld(selectedWorld);
        SceneManager.LoadScene("MainGame");
    }
    
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
    }
    
    public void DeleteGameSave() {
        GameData.Get.DeleteWorld(selectedWorld);
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
            buttonIndex++;
            GameSaveButtonIndices.Add(buttonIndex);
        }
    }
}
