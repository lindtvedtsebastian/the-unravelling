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

		// The cell size guarantees that there is only one point in a given cell.
        float cellSize = Mathf.Floor(radius / Mathf.Sqrt(dimensions));

        // Determine the amount of cells in the grid in total
        int horizontal_cells = Mathf.CeilToInt(width / cellSize) + 1;
        int vertical_cells = Mathf.CeilToInt(height / cellSize) + 1;


		// Instantiate the grid to -1
        int[][] grid = new int[vertical_cells][];
        for (int y = 0; y < vertical_cells; y++) {
            grid[y] = new int[horizontal_cells];
            for (int x = 0; x < horizontal_cells; x++) {
                grid[y][x] = -1;
            }
        }

		

		return points;
    }

	private static void insertPoint(int[][] grid, Vector2 point, int index, float cellsize) {
        int xIndex = Mathf.FloorToInt(point.x / cellsize);
        int yIndex = Mathf.FloorToInt(point.y / cellsize);
        grid[xIndex][yIndex] = index;
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

