using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    private const int maxValue = 100000;
    private const int minValue = -100000;

    public void generate() {
        GenerateNoiseMap(1024,1,30f,6,0.5f,2f,new Vector2(0,0));
    }
    public void GenerateNoiseMap(int mapSize, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[mapSize, mapSize];
        System.Random pseudo_rng = new System.Random(seed);


        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offsetX = pseudo_rng.Next(minValue, maxValue) + offset.x;
            float offsetY = pseudo_rng.Next(minValue, maxValue) + offset.y;
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

        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        string path = Application.dataPath + "/noise.txt";
        StreamWriter writer = new StreamWriter(path);
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                writer.Write(noiseMap[x,y] + " ");
            }
            writer.Write("\n");
        }
        writer.Close();
        Debug.Log("Noise generation complete");
    }
}
