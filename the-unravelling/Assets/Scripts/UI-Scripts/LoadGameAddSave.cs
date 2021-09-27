using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadGameAddSave : MonoBehaviour
{
    private List<string> stringData = new List<string>();
    public GameObject buttonPrefab;
    
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
            GameObject newButton = Instantiate(buttonPrefab) as GameObject;
            newButton.transform.SetParent(this.transform,false);
            Button myButton = newButton.GetComponent<Button>();
            myButton.GetComponentInChildren<TMP_Text>().text = stringData[i];
        }
        Debug.Log("End of start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
