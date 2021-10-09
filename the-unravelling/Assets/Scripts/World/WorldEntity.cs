using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "World Element" ,menuName = "Scriptable Objects/World/World Entity")]
public class WorldEntity : ScriptableObject {
    public int id;
    public Color mapColor;
    public TileBase[] sprites;

    public virtual TileBase SetSprite(int y, int x) {
        return sprites[Random.Range(0, sprites.Length)];
    }
}
