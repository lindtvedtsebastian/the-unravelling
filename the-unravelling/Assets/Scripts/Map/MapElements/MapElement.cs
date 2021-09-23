using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "mapElement" ,menuName = "mapElement")]
public class MapElement : ScriptableObject
{
    public int id;
    public Color mapColor;
    public TileBase[] sprites;
}
