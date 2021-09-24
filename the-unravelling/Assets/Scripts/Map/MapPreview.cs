using UnityEngine;
using UnityEngine.UI;


public class MapPreview : MonoBehaviour
{
    private Texture2D texture;
    public RawImage mapPreview;

    public void drawMap() {
        texture = new Texture2D(WorldData.Get.mapSize, WorldData.Get.mapSize);
        for (int y = 0; y < WorldData.Get.mapSize; y++) {
            for (int x = 0; x < WorldData.Get.mapSize; x++) {
                if (WorldData.Get.map[x,y] == WorldData.Get.GRASS.id) {
                    texture.SetPixel(x, y, WorldData.Get.GRASS.mapColor);
                } else if (WorldData.Get.map[x,y] == WorldData.Get.DIRT.id) {
                    texture.SetPixel(x, y, WorldData.Get.DIRT.mapColor);
                } else {
                    texture.SetPixel(x, y, WorldData.Get.STONE.mapColor);
                }
            }
        }
        texture.Apply();
        
        mapPreview.texture = texture;
    }
}
