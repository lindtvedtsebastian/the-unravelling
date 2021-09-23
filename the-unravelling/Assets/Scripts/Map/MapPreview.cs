using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapPreview : MonoBehaviour
{
    private Texture2D texture;
    public RawImage mapPreview;
    
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(256, 256);

        for (int i = 0; i < 256; i++) {
            texture.SetPixel(i, i, Color.black);
        }
        texture.Apply();
        
        mapPreview.texture = texture;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
