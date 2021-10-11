using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    public GameObject player;
    public Tilemap gameWorld;
    public Tilemap background;
    public Tilemap fog;

    void Start() {
        renderWorld();
    }

    /// <summary>
    /// Renders the world by utilizing the 'map' from the singleton
    /// GameData object. Assigns the correct sprites to all the different
    /// of the map tiles 
    /// </summary>
    public void renderWorld() {
        TileBase tile;
        int halfMapSize = (int)GameData.Get.world.worldSize / 2; // Know that the mapsize is in the power of 2

        player.transform.position = new Vector3Int(halfMapSize, halfMapSize, -10);

        createFog();

        for (int y = 0; y < GameData.Get.world.worldSize; y++) {
            for (int x = 0; x < GameData.Get.world.worldSize; x++) {
                int tileID = GameData.Get.world.map[y, x];
                tile = GameData.Get.worldEntities[tileID].SetSprite(y, x);

                gameWorld.SetTile(new Vector3Int(x, GameData.Get.world.worldSize - y, 0), tile);
                background.SetTile(new Vector3Int(x, GameData.Get.world.worldSize - y, 0),
                                   GameData.Get.worldEntities[GameIDs.STONE].SetSprite(y,x));
            }
        }
        GameData.Get.SaveWorld();
    }

    /// <summary>
    /// Generates a border of "fog" around the world 
    /// </summary>
    /// <param name="fogThickness">How many tiles wide the fog is</param>
    /// <param name="fogColorOffset">The color of the fog
    ///                              0 = Black, 4 = Grey, 8 = Blue, 12 = Purple</param>
    private void createFog(int fogThickness = 4,int fogColorOffset = 0) {
        for (int i = 0; i < 4; i++) { // The amount of fog "layers"
            for (int j = i; j < GameData.Get.world.worldSize - i; j++) { // Drawing each fog layer
                fog.SetTile(new Vector3Int(j,1+i,0), GameData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(i,j+1,0), GameData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(j,GameData.Get.world.worldSize-i,0), GameData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(GameData.Get.world.worldSize-(i+1),j+1,0), GameData.Get.FOG[i+fogColorOffset]);
            }
        }
    }
}
