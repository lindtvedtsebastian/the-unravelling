using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuOpacity : MonoBehaviour {
    [SerializeField] GameObject audioMenu;
    // Start is called before the first frame update
    
    public void ChangeOpacity() {
        // We are unable to modify values in structs returned from other components because of passing by value
        // So we need to modify the component itself then reassign it.
        var temp = audioMenu.GetComponent<Image>().color;
        temp.a = 1.0f;
        audioMenu.GetComponent<Image>().color = temp;
    }
}
