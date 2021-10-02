using UnityEngine;
using UnityEngine.UI;


public class MapPreview : MonoBehaviour
{
    private Texture2D texture;
    public RawImage mapPreview;

    public void drawMap() {
        texture = new Texture2D(GameData.Get.world.worldSize, GameData.Get.world.worldSize);
        for (int y = 0; y < GameData.Get.world.worldSize; y++) {
            for (int x = 0; x < GameData.Get.world.worldSize; x++) {
                if (GameData.Get.world.map[x,y] == GameData.Get.GRASS.id) {
                    texture.SetPixel(x, y, GameData.Get.GRASS.mapColor);
                } else if (GameData.Get.world.map[x,y] == GameData.Get.DIRT.id) {
                    texture.SetPixel(x, y, GameData.Get.DIRT.mapColor);
                } else {
                    texture.SetPixel(x, y, GameData.Get.STONE.mapColor);
                }
            }
        }
        texture.Apply();
        
        mapPreview.texture = texture;
    }
}
