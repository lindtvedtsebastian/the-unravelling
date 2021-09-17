using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class MapGenerator {
    private const int MAXVALUE = 100000;
    private const int MINVALUE = -100000;
    private const int ID_GRASS = 1;
    private const int ID_DIRT  = 2;
    private const int ID_STONE = 3;


    public static void GenerateNoiseMap(int mapSize, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset) {
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

                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;
                noiseMap[x, y] = noiseHeight;
            }
        }

        // Normalizes the noise to be within a 0..1 range
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        string path = Application.dataPath + "/noise.txt";
        StreamWriter writer = new StreamWriter(path);
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                if (noiseMap[x, y] > 0.5f) {
                    //writer.Write(String.Format("{0,2:D2} ",ID_GRASS));
                    writer.Write(String.Format("{0,-2} ",ID_GRASS));
                } else if (noiseMap[x,y] > 0.25f) {
                    //writer.Write(String.Format("{0,2:D2} ",ID_DIRT));
                    writer.Write(String.Format("{0,-2} ",ID_DIRT));
                } else {
                    //writer.Write(String.Format("{0,2:D2} ",ID_STONE));
                    writer.Write(String.Format("{0,-2} ",ID_STONE));
                }
            }
            writer.Write("\n");
        }
        writer.Close();
        Debug.Log("Noise generation complete");
    }
}

[Serializable]
class MapData {
    public int[,] tiledGameWorld;
}