using UnityEngine;
using UnityEngine.UI;


public class MapPreview : MonoBehaviour
{
    private Texture2D texture;
    public RawImage mapPreview;

    /// <summary>
    /// Sets the pixels of a 2D texture to be equivalent to a tilemap 
    /// </summary>
    public void drawMap() {
        texture = new Texture2D(GameData.Get.world.worldSize, GameData.Get.world.worldSize);
        for (int y = 0; y < GameData.Get.world.worldSize; y++) {
            for (int x = 0; x < GameData.Get.world.worldSize; x++) {
                int tileID = GameData.Get.world.map[x, y];
                WorldEntity tile = (WorldEntity) GameData.Get.worldEntities[tileID];
                texture.SetPixel(x, y,tile.mapColor);
           }
        }
        texture.Apply();
        mapPreview.texture = texture;
    }
}
