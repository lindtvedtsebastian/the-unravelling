 using System.Collections;
using System.Collections.Generic;
 using UnityEditor;
 using UnityEngine;
 using UnityEngine.Tilemaps;
using UnityEngine.InputSystem; 
 public class TreeManager : MonoBehaviour {
    [SerializeField] 
    private Tilemap trees;


    private Mouse mouse;


    private void Awake() {
        mouse = Mouse.current;
    }
    
    // Update is called once per frame
    void Update() { 
        Vector3 mousePos = mouse.position.ReadValue();
        if (mouse.enabled) {
            
           
            Vector3Int gridPos = trees.WorldToCell(mousePos);

            TileBase clickedTile = trees.GetTile(gridPos);
            
            print("At position " + gridPos + " there is a " + clickedTile);
        }    
    }
}
