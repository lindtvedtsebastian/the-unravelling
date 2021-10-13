using System;
using UnityEngine;

public static class MapGenerator {
    private const int MAXVALUE = 100000;
    private const int MINVALUE = -100000;

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

        float[,] heightMap = generateNoiseMap(mapSize, seed, scale, octaves, persistance, lacunarity, offset);
        float[,] moistureMap = generateNoiseMap(mapSize, seed+1, scale, octaves, persistance, lacunarity, offset);

        // Assign tiles based on noise
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                if (heightMap[x, y] > 0.4f) {
                    GameData.Get.world.map[x, y] = moistureMap[x,y] >= 0.5f ? GameIDs.GRASS : GameIDs.DIRT;
                    GameData.Get.world.background[x, y] = GameIDs.STONE;
                } else {
                    GameData.Get.world.map[x, y] = GameIDs.STONE;
                    GameData.Get.world.background[x, y] = GameIDs.STONE;
                }
            }
        }
    }

    /// <summary>
    /// Generates a noisemap, that is to be consumed by the tilemap generator 
    /// </summary> 
    /// <param name="mapSize">The size of the map as NxN</param>
    /// <param name="seed">The seed used for generation</param>
    /// <param name="scale">The noise scale</param>
    /// <param name="octaves">How many levels of detail</param>
    /// <param name="persistance">How much each octave contributes to the overall noise</param>
    /// <param name="lacunarity">How much detail each octave adds/removes</param>
    /// <param name="offset">An offset of the noise in (x,y)</param>
    /// <returns>A 2D array of size [mapsize,mapsize] with noise values</returns>
    public static float[,] generateNoiseMap(int mapSize, int seed, float scale, int octaves,
                                     float persistance, float lacunarity, Vector2 offset) {

        float[,] noiseMap = new float[mapSize, mapSize];
        System.Random pseudo_rng = new System.Random(seed);
    
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offsetX = pseudo_rng.Next(MINVALUE, MAXVALUE) + offset.x;
            float offsetY = pseudo_rng.Next(MINVALUE, MAXVALUE) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfSize = mapSize / 2f;
        
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x - halfSize) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfSize) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        // Normalizes the noise to be within a 0..1 range
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        
        return noiseMap;
    }
    
}

