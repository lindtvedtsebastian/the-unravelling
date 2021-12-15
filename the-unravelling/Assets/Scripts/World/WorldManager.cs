using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldManager: MonoBehaviour {
	public World world;

    public GameObject player;

    public GameObject NPCManager;
    public GameObject WorldCore;
    
    public Tilemap gameWorld;
    public Tilemap background;
    public Tilemap fog;

    public Transform entityContainer;

	private void Start() {
		if (GameData.Get.activeWorld != "") {
			world = WorldHandler.loadWorld(GameData.Get.activeWorld);
		}
		else {
			// Should never happen, but if it does, why not generate a new world as backup?
			world = new World("default",UnityEngine.Random.Range(0,999_999_999));
		}
		renderWorld();
	}


    /// <summary>
    /// Renders the world by utilizing the 'map' from the singleton
    /// GameData object. Assigns the correct sprites to all the different
    /// of the map tiles
    /// </summary>
    public void renderWorld() {
        TileBase tile;
        int halfMapSize = (int) world.size / 2; // Know that the mapsize is in the power of 2

        player.transform.position = new Vector3Int(halfMapSize, halfMapSize, -10);

        createFog();

        TileEntity stone = (TileEntity) GameData.Get.worldEntities[Constants.STONE];

        for (int y = 0; y < world.size; y++) {
            for (int x = 0; x < world.size; x++) {
                int tileID = world.terrain[y][x];
                TileEntity tileData = (TileEntity) GameData.Get.worldEntities[tileID];
                tile = tileData.SetSprite(y, x);

                gameWorld.SetTile(new Vector3Int(x, world.size-1 - y, 0), tile);
                background.SetTile(new Vector3Int(x, world.size-1 - y, 0),
                                   stone.SetSprite(y,x));

                int entityID = world.entities[y][x];
                if (entityID != 0) {
	                WorldEntity worldEntity = (WorldEntity) GameData.Get.worldEntities[entityID];
	                GameObject entity = worldEntity.manifestation;
	                Vector3 entityPos = new Vector3(x + .5f, world.size - y + .5f, 0);
	                Instantiate(entity, entityPos, Quaternion.identity, entityContainer);
                }
            }
        }

        Instantiate(NPCManager, Vector3.zero, quaternion.identity);
        Instantiate(WorldCore, Vector3.zero, quaternion.identity);

        
		WorldHandler.saveWorld(world);
    }


    /// <summary>
    /// Generates a border of "fog" around the world
    /// </summary>
    /// <param name="fogThickness">How many tiles wide the fog is</param>
    /// <param name="fogColorOffset">The color of the fog
    ///                              0 = Black, 4 = Grey, 8 = Blue, 12 = Purple</param>
    private void createFog(int fogThickness = 4,int fogColorOffset = 0) {
        for (int i = 0; i < 4; i++) { // The amount of fog "layers"
            for (int j = i; j < world.size - i; j++) { // Drawing each fog layer
                fog.SetTile(new Vector3Int(j,1+i,0), GameData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(i,j+1,0), GameData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(j,world.size-i,0), GameData.Get.FOG[i+fogColorOffset]);
                fog.SetTile(new Vector3Int(world.size-(i+1),j+1,0), GameData.Get.FOG[i+fogColorOffset]);
            }
        }
    }


    public void regenerateResources() {
	    for (int y = 0; y < world.size; y++) {
		    for (int x = 0; x < world.size; x++) {
			    int newResourceID = world.baseResourceLocations[y][x];
			    if (world.entities[y][x] == 0 &&  newResourceID != 0) {
				    int regen = UnityEngine.Random.Range(0, 2);
				    if (regen <= 1) {
					    world.entities[y][x] = newResourceID;
					    WorldEntity worldEntity = (WorldEntity) GameData.Get.worldEntities[newResourceID];
					    GameObject entity = worldEntity.manifestation;
                        Vector3 entityPos = new Vector3(x + .5f, world.size-1 - y + .5f, 0);
                        Instantiate(entity, entityPos, Quaternion.identity, entityContainer);
				    }
			    }
		    }
	    }
    }
}
