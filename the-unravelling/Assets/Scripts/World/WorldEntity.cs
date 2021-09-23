using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "World Element" ,menuName = "Scriptable Objects/World/World Entity")]
public class WorldEntity : ScriptableObject {
    public int id;
    public Color mapColor;
    public TileBase[] sprites;
}
