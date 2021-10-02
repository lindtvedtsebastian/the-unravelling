using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MapGenerator {
    private const int MAXVALUE = 100000;
    private const int MINVALUE = -100000;

    public static string currentMapPath;


    public static void GenerateTilemap(string newMapName,int mapSize, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset) {
        GameData.Get.world.mapName = newMapName;
        GameData.Get.world.worldSize = mapSize;
        GameData.Get.world.map = new int[mapSize, mapSize];
        
        float[,] heightMap = generateNoiseMap(mapSize, seed, scale, octaves, persistance, lacunarity, offset);
        float[,] moistureMap = generateNoiseMap(mapSize, seed+1, scale, octaves, persistance, lacunarity, offset);

        // Assign tiles based on noise
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                if (heightMap[x, y] > 0.5f) {
                    GameData.Get.world.map[x, y] = GameData.Get.GRASS.id;
                } else if (heightMap[x,y] > 0.25f) {
                    GameData.Get.world.map[x, y] = GameData.Get.DIRT.id;
                } else {
                    GameData.Get.world.map[x, y] = GameData.Get.STONE.id;
                }
            }
        }
    }
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

