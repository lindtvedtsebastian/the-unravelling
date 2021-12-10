using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
    private const int LOWLAND = 0;
    private const int HIGHLAND = 10;
    private const int DRY = 0;
    private const int MOIST = 5;

    public static IWorld generateWorld(int size = 256, int seed = 123) {
        IWorld world = new IWorld(size);
        Vector2 offset = new Vector2(0, 0);
        float[][] heightMap = Noise.generateNoiseMap(seed:seed++, offset: offset);
        float[][] moistureMap = Noise.generateNoiseMap(seed: seed++, offset: offset);



        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                world.terrain[y][x] = determineBiome(heightMap[y][x], moistureMap[y][x]);
            }
        }

        List<Vector2> resourceSamples = PoissonDisc.sample(size,size,world.terrain);
		
		return world;
    }


    private static int determineBiome(float height, float moisture) {
        int biome = height < 0.4f ? LOWLAND : HIGHLAND;
        biome += moisture < 0.5f ? DRY : MOIST;

		if (biome < 10) return GameIDs.STONE;
		else if (biome == 10) return GameIDs.DIRT;
		else return GameIDs.GRASS;
    }
}

[Serializable]
public class IWorld {
    public WorldState state;
    public int size;
    public int[][] terrain;
    public int[][] entities;
    public int[][] pathfindingMap;
    public string mapName;

    public IWorld(int size) {
        state = new WorldState();
        this.size = size;
        terrain = JaggedArrayUtility.createJagged2dArray<int>(size, size);
        entities = JaggedArrayUtility.createJagged2dArray<int>(size, size);
        pathfindingMap = JaggedArrayUtility.createJagged2dArray<int>(size, size);
    }
}

