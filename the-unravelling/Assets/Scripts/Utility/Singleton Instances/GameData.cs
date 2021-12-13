using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Data")]
public class GameData : ScriptableObjectSingleton<GameData> {
    public string activeWorld;

    public Entity[] worldEntities;
    public TileBase[] FOG;
}


