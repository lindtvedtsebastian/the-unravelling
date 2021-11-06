using System;
using UnityEngine;

public static class MapGenerator {

    public static string currentMapPath;


    /// <summary>
    /// Generates a new map with different tileIDs, which functions as a game world
    /// </summary>
    /// <param name="newMapName">The name of the map/world</param>
    /// <param name="mapSize">The size of the map as NxN</param>
    /// <param name="seed">The seed used for generation</param>
    /// <param name="scale">The noise scale</param>
    /// <param name="octaves">How many levels of detail</param>
    /// <param name="persistance">How much each octave contributes to the overall noise</param>
    /// <param name="lacunarity">How much detail each octave adds/removes</param>
    /// <param name="offset">An offset of the noise in (x,y)</param>
    public static void GenerateTilemap(string newMapName,int mapSize, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset) {
        
        GameData.Get.world.mapName = newMapName != "" ? newMapName : "autosave_"+DateTime.Now.ToString("dd-MM-yyyy_HHmm");
        GameData.Get.world.worldSize = mapSize;
        GameData.Get.world.map = new int[mapSize, mapSize];
        GameData.Get.world.background = new int[mapSize, mapSize];
        GameData.Get.world.pathfindingMap = new int[mapSize, mapSize];
        GameData.Get.world.state = new WorldState();

        float[,] heightMap = Noise.generateNoiseMap(mapSize, seed, scale, octaves, persistance, lacunarity,1, offset);
        float[,] moistureMap = Noise.generateNoiseMap(mapSize, seed + 1, scale, octaves, persistance, lacunarity,1, offset);
		float[,] resourceClusters = Noise.generateNoiseMap(mapSize, seed + 2, scale, octaves, persistance, lacunarity,1, offset);
        float[,] resourceDistribution = Noise.generateNoiseMap(mapSize, seed + 3, 20, 1, 1, 1, 10, offset);


        GameData.Get.world.iEntities.Clear();
		

        // Assign tiles based on noise
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                GameData.Get.world.background[x, y] = GameIDs.STONE;
                GameData.Get.world.pathfindingMap[y, x] = 0; // Sets to 0 as a base, will be updated if necessary

                if (heightMap[y, x] > 0.4f) {
                    if (moistureMap[x, y] >= 0.5f) {
                        GameData.Get.world.map[y, x] = GameIDs.GRASS;


                        if (resourceClusters[x, y] >= 0.10f) {
							double max = findMaxAround(x, y, 5, resourceDistribution);
							if (resourceDistribution[x,y] == max) {
                                GameData.Get.world.iEntities.Add(new IEntity(x, GameData.Get.world.worldSize - y, GameIDs.TREE));
								GameData.Get.world.pathfindingMap[y, x] = 9999;
                            }
						}
                    }
                    else {
                        GameData.Get.world.map[y, x] = GameIDs.DIRT;
                        if (resourceClusters[x, y] >= 0.30f) {
							double max = findMaxAround(x, y, 8, resourceDistribution);
							if (resourceDistribution[x,y] == max) {
                                GameData.Get.world.iEntities.Add(new IEntity(x,GameData.Get.world.worldSize - y,GameIDs.DRY_TREE));
								GameData.Get.world.pathfindingMap[y, x] = 9999;
							}
						}
                    }
                } else {
                    GameData.Get.world.map[y, x] = GameIDs.STONE;
					if (resourceClusters[x,y] > 0.50f) {
                        double max = findMaxAround(x, y, 3, resourceDistribution);
						if (resourceDistribution[x,y] == max) {
                            GameData.Get.world.iEntities.Add(new IEntity(x, GameData.Get.world.worldSize - y, determineOreType()));
							GameData.Get.world.pathfindingMap[y, x] = 9999;
                        }
                    }
                }
            }
        }
    }

	public static int determineOreType() {
        int rand = UnityEngine.Random.Range(0, 10);
		if (rand < 6)
            return GameIDs.STONE;
		else if (rand < 8)
            return GameIDs.COPPER_ORE;
		else
            return GameIDs.IRON_ORE;
    }

    /// <summary>
    /// Determines the maximum noise value in an area around the coordinate
    /// </summary>
    /// <param name="x">The base x coordinate</param>
    /// <param name="y">The base y coordinate</param>
    /// <param name="radius">The radius around to check</param>
    /// <param name="noise">The noise array to check in</param>
    /// <returns>The highest noise value in the area</returns>
    public static double findMaxAround(int x, int y, int radius, float[,] noise) {
        double max = 0;
        for (int iy = y - radius; iy <= y + radius; iy++) {
            for (int ix = x - radius; ix <= x + radius; ix++) {
				if (insideMap(iy,ix)) {
					double value = noise[iy, ix];
                    max = value > max ? value : max;
                }
			}
        }
        return max;
    }

    /// <summary>
    /// Checks if a coordinate x,y is inside the map
    /// </summary>
	/// <param name="x">The x coordinate</param>
    /// <param name="y">The y coordinate</param>
    /// <returns>Wheter or not the coord is inside the map</returns>
    public static bool insideMap(int x, int y) {
        int size = GameData.Get.world.worldSize;
        return (x >= 0 && x < size && y >= 0 && y < size);
    }
}

