using UnityEngine;
using UnityEngine.Tilemaps;


public class BiomeArea {
    private int[][] terrain;
    private int size;
    private int[][,] biomes;
    private int[][,] biggestBiomes;
    private bool[,,] visited;
        
    
    BiomeArea() {
        var WM = GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>();
        terrain = WM.world.terrain;
        size = WM.world.size;
        biomes = new int[3][,];
        visited = new bool[3,size,size];
    }


    /// <summary>
    /// Finds the biggest biomes
    /// </summary>
    private void FindBiggestBiome() {
        for (int t = 0; t < 3; t++) {
            for (int y = 1; y < size; y++) {
                for (int x = 1; x < size; x++) {
                    if (visited[t, y, x]) {
                        continue;
                    }

                    if (CalculateBiomeArea(0, 0, t) < biggestBiomes[t].Length)
                        biggestBiomes[t] = biomes[t];
                }
            }
        }
    }
    
    /// <summary>
    /// Adds x,y coordinates of terrain in biome to list
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="tileType">Type of terrain</param>
    private int CalculateBiomeArea(int x, int y, int terrainType) {
        
        //If cell has not been visited
        if (!visited[terrainType, y, x]) {
            visited[terrainType, y,x] = true;

            //If inside boarders and terrain is of correct type
            if (terrain[y][x] == terrainType && x < size && x > 0 && y < size && y > 0) {
                biomes[terrainType] = new int[x,y];
                
                //Check neighbours 
                //Row above                                        
                return 1 + CalculateBiomeArea(x, y + 1, terrainType) +
                       CalculateBiomeArea(x + 1, y + 1, terrainType) +
                       CalculateBiomeArea(x - 1, y + 1, terrainType) +
                       //Row below
                       CalculateBiomeArea(x - 1, y - 1, terrainType) +
                       CalculateBiomeArea(x + 1, y - 1, terrainType) +
                       CalculateBiomeArea(x, y - 1, terrainType) +
                       //R/L
                       CalculateBiomeArea(x + 1, y, terrainType) +
                       CalculateBiomeArea(x - 1, y, terrainType);

            }    
        }

        return 0;
    }
    
    
}


public class NonPlayerCharacter : MonoBehaviour {
    
    
}
