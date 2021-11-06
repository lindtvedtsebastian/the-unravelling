using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {
	private const int MAXVALUE = 100000;
    private const int MINVALUE = -100000;

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
    public static float[][] generateNoiseMap(Vector2 offset,int mapSize = 256, int seed = 10,
											float scale = 1,int octaves = 3,float persistance = 0.5f,
											float lacunarity = 2f, float startFrequency = 1) {

        float[][] noiseMap = createJagged2dArray<float>(mapSize,mapSize);
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
                float frequency = startFrequency;
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
                noiseMap[x][y] = noiseHeight;
            }
        }

        // Normalizes the noise to be within a 0..1 range
        for (int y = 0; y < mapSize; y++) {
            for (int x = 0; x < mapSize; x++) {
                noiseMap[x][y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x][y]);
            }
        }
        return noiseMap;
    }

	/// <summary>
    /// Creates a NxM jagged int array 
    /// </summary>
    /// <param name="width">The width of the array</param>
    /// <param name="height">The height of the array</param>
    /// <returns>The new jagged 2d array</returns>
    public static T[][] createJagged2dArray<T>(int width, int height) {
        T[][] array = new T[height][];
        for (int i = 0; i < array.Length; i++) {
            array[i] = new T[width];
        }
        return array;
    }
}
