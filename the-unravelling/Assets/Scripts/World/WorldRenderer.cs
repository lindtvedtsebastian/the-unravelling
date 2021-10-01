using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    public GameObject mainCamera;
    public Tilemap gameWorld;
    public Tilemap background;
    public Tilemap fog;

    // Start is called before the first frame update
    void Start()
    {
        TileBase tile = WorldData.Get.GRASS.sprites[0];
        int halfMapSize = (int)WorldData.Get.world.worldSize / 2; // Know that the mapsize is in the power of 2

        mainCamera.transform.position = new Vector3Int(halfMapSize, halfMapSize, -10);

        createFog(12); //0 = Black, 4 = Grey, 8 = Blue, 12 = Purple

        for (int y = 0; y < WorldData.Get.world.worldSize; y++) {
            for (int x = 0; x < WorldData.Get.world.worldSize; x++) {
                // Find a much better way of doing this, this is not scalable
                switch (WorldData.Get.world.map[y,x]) {
                    case 1: tile = WorldData.Get.GRASS.SetSprite(y,x); break;
                    case 2: tile = WorldData.Get.DIRT.SetSprite(); break;
                    case 3: tile = WorldData.Get.STONE.SetSprite(); break;
                }
                gameWorld.SetTile(new Vector3Int(x, WorldData.Get.world.worldSize - y, 0), tile);
                background.SetTile(new Vector3Int(x, WorldData.Get.world.worldSize - y, 0),
                                   WorldData.Get.DIRT.SetSprite());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createFog(int fogColorOffset = 0) {
        for (int i = 0; i < 4; i++) { // The amount of shadow "layers"
            for (int j = i; j < WorldData.Get.world.worldSize - i; j++) { // Drawing each shadow layer
                fog.SetTile(new Vector3Int(j,1+i,0), WorldData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(i,j+1,0), WorldData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(j,WorldData.Get.world.worldSize-i,0), WorldData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(WorldData.Get.world.worldSize-(i+1),j+1,0), WorldData.Get.FOG[i+fogColorOffset]);
            }
        }
    }
}
