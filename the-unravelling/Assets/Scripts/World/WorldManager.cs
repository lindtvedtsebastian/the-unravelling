using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldManager: MonoBehaviour {
	private IWorld world;

    public GameObject player;

    public Tilemap gameWorld;
    public Tilemap background;
    public Tilemap fog;

	private void Start() {
		if (GameData.Get.activeWorld != "") {
			world = WorldHandler.loadWorld(GameData.Get.activeWorld);
		}
		else {
			// Should never happen, but if it does, why not generate a new world as backup?
			world = new IWorld("default",UnityEngine.Random.Range(0,100_000));
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

        for (int y = 0; y < world.size; y++) {
            for (int x = 0; x < world.size; x++) {
                int tileID = world.terrain[y][x];
                WorldEntity tileData = (WorldEntity) GameData.Get.worldEntities[tileID];
                WorldEntity stone = (WorldEntity) GameData.Get.worldEntities[Constants.STONE];
                tile = tileData.SetSprite(y, x);

                gameWorld.SetTile(new Vector3Int(x, world.size - y, 0), tile);
                background.SetTile(new Vector3Int(x, world.size - y, 0),
                                   stone.SetSprite(y,x));
            }
        }

		foreach (IEntity ientity in GameData.Get.world.iEntities) {
            int id = ientity.entityID;
            GameObject entity = GameData.Get.worldEntities[ientity.entityID].manifestation;
            Vector3 pos = new Vector3(ientity.worldPosX + .5f, ientity.worldPosY + .5f, 0);
            Instantiate(entity, pos, Quaternion.identity, IEntityContainer.transform);
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
