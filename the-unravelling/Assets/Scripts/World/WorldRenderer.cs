using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    public Tilemap gameWorld;
    public Tilemap background;
    public Tilemap fog;

    // Start is called before the first frame update
    void Start()
    {
        TileBase tile = WorldData.Get.GRASS.sprites[0];
        int halfMapSize = (int)WorldData.Get.worldSize / 2; // Know that the mapsize is in the power of 2
        for (int y = 0; y < WorldData.Get.worldSize; y++) {
            for (int x = 0; x < WorldData.Get.worldSize; x++) {
                // Find a much better way of doing this, this is not scalable
                switch (WorldData.Get.map[y,x]) {
                    case 1: tile = WorldData.Get.GRASS.SetSprite(y,x); break;
                    case 2: tile = WorldData.Get.DIRT.SetSprite(); break;
                    case 3: tile = WorldData.Get.STONE.SetSprite(); break;
                }
                gameWorld.SetTile(new Vector3Int(x - halfMapSize, WorldData.Get.worldSize - y - halfMapSize, 0), tile);
                background.SetTile(new Vector3Int(x - halfMapSize, WorldData.Get.worldSize - y - halfMapSize, 0),
                                   WorldData.Get.DIRT.SetSprite());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createFog() {
        
    }
}
