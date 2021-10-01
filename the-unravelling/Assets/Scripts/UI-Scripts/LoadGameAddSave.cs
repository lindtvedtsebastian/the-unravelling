using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using System.IO;

public class LoadGameAddSave : MonoBehaviour
{
    private List<string> stringData = new List<string>();
    private DirectoryInfo dir;
    private FileInfo[] files;
    public GameObject buttonPrefab;
    public RawImage previewImage;
    

    void Start() {
        dir = new DirectoryInfo(Application.persistentDataPath);
        files = dir.GetFiles("*.png");

        foreach (FileInfo file in files) { // fill the entries with random data for now until savefiles are a thing.
            stringData.Add(file.Name.Replace(".png",""));
        }

        for (int i = 0; i < stringData.Count; i++) {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform,false);
            newButton.name = stringData[i];
            newButton.GetComponentInChildren<TMP_Text>().text = stringData[i];
            newButton.GetComponent<Button>().onClick.AddListener(SelectSave);
        }
    }

    /// <summary>
    /// SelectSave triggers the event for changing the preview image and sets the current selected button as savefile to load.
    /// </summary>
    public void SelectSave() {
        // byte[] image =
        previewImage.GetComponent<RawImage>().color = Color.gray;
        // ImageConversion.LoadImage("");
    }
}
