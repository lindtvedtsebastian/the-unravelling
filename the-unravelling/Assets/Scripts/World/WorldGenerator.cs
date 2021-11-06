using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
	public static IWorld generateWorld(int size = 256, int seed = 123) {
        IWorld world = new IWorld();
        world.size = size;
        float[][] terrainNoise = Noise.generateNoiseMap(seed:seed, offset: new Vector2(0,0));

		
		
        return world;
    }

}

public static class PoissonDisc {
	public static List<Vector2> sample(float radius,int width, int height, int rejectionRate = 30, int dimensions = 2) {
        List<Vector2> points = new List<Vector2>();

        float cellSize = Mathf.Floor(radius / Mathf.Sqrt(dimensions));

        // Determine the amount of cells in the grid in total
        int horizontal_cells = Mathf.CeilToInt(width / cellSize) + 1;
        int vertical_cells = Mathf.CeilToInt(height / cellSize) + 1;


		// Instantiate the grid to -1
        int[][] grid = new int[height][];
        for (int y = 0; y < height; y++) {
            grid[y] = new int[width];
            for (int x = 0; x < width; x++) {
                grid[y][x] = -1;
            }
        }

		

		return points;
    }
}


[Serializable]
public class IWorld {
    public WorldState state;
    public int size;
    public int[][] terrain;
    public int[][] background;
    public int[][] pathfindingMap;
    public List<IEntity> iEntities;
    public string mapName;

    public IWorld() {
        state = new WorldState();
        iEntities = new List<IEntity>();
    }
}

