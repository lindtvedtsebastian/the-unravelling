using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    public Tilemap gameWorld;
    public TileBase[] grassTiles;
    // Start is called before the first frame update
    void Start()
    {
        for (int y = -16; y < 16; y++) {
            for (int x = -16; x < 16; x++) {
                gameWorld.SetTile(new Vector3Int(x,y,0),grassTiles[Random.Range(0,15)]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
