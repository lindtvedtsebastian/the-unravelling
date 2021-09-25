using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "World Element" ,menuName = "Scriptable Objects/World/World Entity")]
public class WorldEntity : ScriptableObject {
    public int id;
    public Color mapColor;
    public TileBase[] baseSprites;

    public TileBase GetRandomBaseSprite() {
        return baseSprites[Random.Range(0, baseSprites.Length)];
    }
}
