using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "World Element" ,menuName = "Scriptable Objects/World/World Entity")]
public class WorldEntity : ScriptableObject {
    public int id;
    public Color mapColor;
    public TileBase[] sprites;
    
    /// <summary>
    /// Base function for setting a tile sprite at a world coordinate [x,y]
    /// </summary>
    /// <param name="y">The tile's position on the y axis</param>
    /// <param name="x">The tile's position on the x axis</param>
    /// <returns></returns>
    public virtual TileBase SetSprite(int y, int x) {
        return sprites[Random.Range(0, sprites.Length)];
    }
}
