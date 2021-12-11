using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGenerator : MonoBehaviour {
    private const int LOWLAND = 0;
    private const int HIGHLAND = 10;
    private const int DRY = 0;
    private const int MOIST = 5;

    public static World generateWorld(string worldName, int size = 256, int seed = 123) {
        worldName = worldName != "" ? worldName : "autosave_"+DateTime.Now.ToString("dd-MM-yyyy_HHmm");
        World world = new World(worldName,size);
        Vector2 offset = new Vector2(0, 0);
        float[][] heightMap = Noise.generateNoiseMap(seed:seed++, offset: offset);
        float[][] moistureMap = Noise.generateNoiseMap(seed: seed++, offset: offset);


        // Determine the biome for different regions on the map
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                world.terrain[y][x] = determineBiome(heightMap[y][x], moistureMap[y][x]);
            }
        }

        // Sample resource distributions
        List<Vector2> resourceSamples = PoissonDisc.sample(size,size,world.terrain);

        // Convert points to resource locations
        foreach (Vector2 point in resourceSamples) {
            int x = Mathf.FloorToInt(point.x);
            int y = Mathf.FloorToInt(point.y);
            int resourceID = assignResourceID(world.terrain[y][x]);
            world.baseResourceLocations[y][x] = resourceID;
            world.entities[y][x] = resourceID;
            world.pathfindingMap[y][x] = 9999;
        }
		return world;
    }


    private static int determineBiome(float height, float moisture) {
        int biome = height < 0.4f ? LOWLAND : HIGHLAND;
        biome += moisture < 0.5f ? DRY : MOIST;

		if (biome < 10) return Constants.STONE;
		else if (biome == 10) return Constants.DIRT;
		else return Constants.GRASS;
    }

    private static int assignResourceID(int biome) {
        switch (biome) {
            case Constants.STONE: return determineOreType();
            case Constants.DIRT: return Constants.DRY_TREE;
            case Constants.GRASS: return Constants.TREE;
            default: return Constants.STONE_ORE;
        }
    }

    public static int determineOreType() {
        int ore = UnityEngine.Random.Range(0, 30);
        if (ore <= 15)
            return Constants.STONE_ORE;
        else if (ore <= 25)
            return Constants.COPPER_ORE;
        else
            return Constants.IRON_ORE;
        }
}

[Serializable]
public class World {
    public WorldState state;
    public int size;
    public int[][] terrain;
    public int[][] entities;
    public int[][] pathfindingMap;
    public int[][] baseResourceLocations;
    public string worldName;

    public World(string name,int size) {
        worldName = name;
        state = new WorldState();
        this.size = size;
        terrain = JaggedArrayUtility.createJagged2dArray<int>(size, size);
        entities = JaggedArrayUtility.createJagged2dArray<int>(size, size);
        pathfindingMap = JaggedArrayUtility.createJagged2dArray<int>(size, size);
        baseResourceLocations = JaggedArrayUtility.createJagged2dArray<int>(size, size);
    }


}


