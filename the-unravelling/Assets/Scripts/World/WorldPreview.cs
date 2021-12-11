using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldPreview : MonoBehaviour {
    private Texture2D texture;
    public RawImage mapPreview;

    /// <summary>
    /// Sets the pixels of a 2D texture to be equivalent to a tilemap
    /// </summary>
    public void drawMap(World world) {
        texture = new Texture2D(world.size, world.size);
        for (int y = 0; y < world.size; y++) {
            for (int x = 0; x < world.size; x++) {
                int tileID = world.terrain[y][x];
                WorldEntity tile = (WorldEntity) GameData.Get.worldEntities[tileID];
                texture.SetPixel(x, y,tile.mapColor);
            }
        }
        texture.Apply();
        mapPreview.texture = texture;
    }
}
