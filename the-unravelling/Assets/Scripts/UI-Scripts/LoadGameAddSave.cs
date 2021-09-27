using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class LoadGameAddSave : MonoBehaviour
{
    private List<string> stringData = new List<string>(); // this will be replaced with the savefiles which will be retrieved programatically.
    public GameObject buttonPrefab;
    public Image previewImage;

    void Start() {

        stringData.Add("first entry");
        stringData.Add("second entry");
        stringData.Add("third entry");
        stringData.Add("fourth entry");
        stringData.Add("fifth entry");
        stringData.Add("sixth entry");
        stringData.Add("seventh entry");
        stringData.Add("eight entry");
        stringData.Add("ninth entry");
        stringData.Add("tenth entry");
        
        
        for (int i = 0; i < stringData.Count; i++) {
            buttonPrefab.SetActive(true);
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(this.transform,false);
            newButton.GetComponentInChildren<TMP_Text>().text = stringData[i];
            
            Button myButton = newButton.GetComponent<Button>();
            myButton.name = stringData[i];
            myButton.onClick.AddListener(SelectSave);
        }
        Debug.Log("End of start");
    }

    /// <summary>
    /// SelectSave triggers the event for changing the preview image and sets the current selected button as savefile to load.
    /// </summary>
    public void SelectSave() {
        // Every button would need to retrieve based on some parameter or identifier, it's corresponding preview image.
        previewImage.GetComponent<Image>().color = Color.cyan;
        Debug.Log("Button was clicked");
    }
    
}
