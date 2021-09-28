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


    public static void GenerateNoiseMap(string newMapName,int mapSize, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset) {
        WorldData.Get.mapName = newMapName;
        WorldData.Get.worldSize = mapSize;
        WorldData.Get.map = new int[mapSize, mapSize];
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

        // Assign tiles based on noise
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                if (noiseMap[x, y] > 0.5f) {
                    WorldData.Get.map[x, y] = WorldData.Get.GRASS.id;
                } else if (noiseMap[x,y] > 0.25f) {
                    WorldData.Get.map[x, y] = WorldData.Get.DIRT.id;
                } else {
                    WorldData.Get.map[x, y] = WorldData.Get.STONE.id;
                }
            }
        }
        //Not sensible to save the map here anymore as the user might not actually want this map

        // SaveMap(tiledGameWorld, newMapName);
        // MapGenerator.currentMapPath = Application.persistentDataPath + "/" + newMapName + ".dat";
        // Debug.Log("Tiled game world saved to: " + Application.persistentDataPath);
    }

    static void SaveMap(int[,] tiledGameWorld, string filename = "game-world") {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create(Application.persistentDataPath + "/" + filename + ".dat");
        MapData data = new MapData();
        data.tiledGameWorld = tiledGameWorld;
        bf.Serialize(saveFile,data);
        saveFile.Close();
    }

    static MapData LoadMap(string filename = "game-world") {
        if (File.Exists(Application.persistentDataPath + "/" + filename + ".dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream loadFile = File.Open(Application.persistentDataPath + "/" + filename + ".dat", FileMode.Open);
            return (MapData) bf.Deserialize(loadFile);
        }
        return null;
    }

    [Serializable]
    class MapData {
        public int[,] tiledGameWorld;
    }
}
