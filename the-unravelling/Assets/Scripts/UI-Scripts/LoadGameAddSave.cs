using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class LoadGameAddSave : MonoBehaviour
{
    private List<string> stringData = new List<string>(); // this will be replaced with the savefiles which will be retrieved programatically.
    public GameObject buttonPrefab;
    public RawImage previewImage;

    void Start() {

        for (char c = 'A'; c <= 'Z'; c++) { // fill the entries with random data for now until savefiles are a thing.
            stringData.Add(c.ToString());
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
        // Every button would need to retrieve based on some parameter or identifier, it's corresponding preview image for that map.
        previewImage.GetComponent<RawImage>().color = Color.cyan;
        Debug.Log("Button was clicked");
    }
}
