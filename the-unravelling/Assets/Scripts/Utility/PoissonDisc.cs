using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class PoissonDisc {
    const float R_MAX = 6;
    const float R_MIN = 3;

    /// <summary>
    /// Samples/creates a list of points, evenly distributed.
    /// </summary>
    /// <param name="width">The width of the space to generate points within</param>
    /// <param name="height">The height of the space to generate points within</param>	    
    /// <param name="terrain">The jagged 2d array containing biomes</param>
    /// <param name="rejectionRate">How many tries each point sampling should be given before rejecting the point</param>
    /// <param name="dimensions">In how many dimensions the points should be generated in, default 2d</param>
    /// <returns>The list of generated/sampled points</returns>	
    public static List<Vector2> sample(int width, int height, int[][] terrain, int rejectionRate = 30, int dimensions = 2) {
        // The current index
        int index = 0;

        // The resulting list of points that we are going to return
        List<Vector2> points = new List<Vector2>();

        // The list of active points, to be checked
        List<Vector2> activePoints = new List<Vector2>();

        // The cell size guarantees that there is only one point in a given cell.
        float cellSize = Mathf.Floor(R_MAX / Mathf.Sqrt(dimensions));

        // Determine the amount of cells in the grid in total
        int horizontalCells = Mathf.CeilToInt(width / cellSize) + 1;
        int verticalCells = Mathf.CeilToInt(height / cellSize) + 1;


		// Instantiate the grid to -1
        int[][] grid = new int[verticalCells][];
        for (int y = 0; y < verticalCells; y++) {
            grid[y] = new int[horizontalCells];
            for (int x = 0; x < horizontalCells; x++) {
                grid[y][x] = -1; 
            }
        }

        // The initial sampling point to somewhere within our "map"
        Vector2 p0 = new Vector2(Random.Range(0, width), Random.Range(0, height));

        // Add the point to the grid and the lists
        insertPoint(grid, p0, index++, cellSize);
        points.Add(p0);
        activePoints.Add(p0);

        // // While there still exists points to be checked
        while (activePoints.Count > 0) {
            // Pick a random index from the active list 
            int randomIndex = Random.Range(0, activePoints.Count-1);
			// Fetch the equivalent point
            Vector2 point = activePoints[randomIndex];

            // The boolean for determining if we found a valid point.
            bool found = false;
            for (int attempts = 0; attempts < rejectionRate; attempts++) {
                // Fetch this point's radius
                float currentRadius = biomeToSampleDistance(terrain[Mathf.FloorToInt(point.y)][Mathf.FloorToInt(point.x)]);
                // Create a new random point
                float theta = Random.Range(0, 360);
                float newRadius = Random.Range(currentRadius, currentRadius * 2);
                float x = point.x + newRadius * Mathf.Cos(Mathf.Deg2Rad * theta);
                float y = point.y + newRadius * Mathf.Sin(Mathf.Deg2Rad * theta);
                Vector2 newPoint = new Vector2(x, y);

				// If not valid, continue to next loop iteration
                if (isValidPoint(grid, points, horizontalCells, verticalCells,
								 height, width, cellSize, newPoint, newRadius)) {
					
                    insertPoint(grid, newPoint, index++, cellSize);
                    points.Add(newPoint);
                    activePoints.Add(newPoint);
                    found = true;
                    break;
				}
			}

            // If no new points was found around the point, remove it
            if (!found)
                activePoints.RemoveAt(randomIndex);
        }

        return points;
    }

    	/// <summary>
    	/// Determines wheter or not a point is valid
    	/// </summary>
    	/// <param name="points">The list of generated points</param>
    	/// <param name="horizontalCellCount">The amount of cells horizontally</param>	    
    	/// <param name="verticalCellCount">The amount of cells vertically</param>
    	/// <param name="height">The height of the space to generate points within</param>
    	/// <param name="width">The width of the space to generate points within</param>
    	/// <param name="cellSize">The size of any given cell</param>
    	/// <param name="point">The point to check if is valid</param>
	/// <param name="radius">The radius around the point which there should be no other points</param>
    	/// <returns>Wheter the point is valid or not</returns>	
	private static bool isValidPoint(int[][] grid,
									 List<Vector2> points,
									 int horizontalCellCount,
									 int verticalCellCount,
									 int height,
									 int width,
									 float cellSize,
									 Vector2 point, float radius) {
		
		// Check if the point even is inside the screen
		if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height)
            return false;

		// Determine the grid index of the point
		int xIndex = Mathf.FloorToInt(point.x / cellSize);
        int yIndex = Mathf.FloorToInt(point.y / cellSize);

        // If the neighbours is outside of the grid, start on edge instead
        int startX = Mathf.Max(xIndex - 1, 0);
        int startY = Mathf.Max(yIndex - 1, 0);
        int endX = Mathf.Min(xIndex + 1, horizontalCellCount - 1);
        int endY = Mathf.Min(yIndex + 1, verticalCellCount - 1);

        for (int y = startY; y < endY; y++) {
            for (int x = startX; x < endX; x++) {
                int neighourIndex = grid[y][x];
                if (neighourIndex >= 0) // Is there even a point to check against?
					if (Vector2.Distance(new Vector2(point.x,point.y),points[neighourIndex]) < radius)
                        return false;
            }
        }
        return true;
    }

	/// <summary>
    	/// Inserts a point into the grid
    	/// </summary>
    	/// <param name="grid">The grid to place the point into</param>
    	/// <param name="point">The point to insert</param>	    
    	/// <param name="index">the index of the point to be inserted</param>
    	/// <param name="cellsize">The size of any given cell</param>	
	private static void insertPoint(int[][] grid, Vector2 point, int index, float cellSize) {
        int xIndex = Mathf.FloorToInt(point.x / cellSize);
        int yIndex = Mathf.FloorToInt(point.y / cellSize);
        grid[yIndex][xIndex] = index;
    }

	/// <summary>
    	/// Converts a biome into a radius
    	/// </summary>
    	/// <param name="biome">The biome ID</param>
	/// <returns>The radius for a given biome<returns>
	private static float biomeToSampleDistance(int biome) {
		switch (biome) {
			case 1: return 6;
			case 2: return 3;
			default: return 4;
        }
	}
}
