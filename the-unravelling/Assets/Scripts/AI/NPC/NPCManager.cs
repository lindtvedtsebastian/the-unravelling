using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {
    public int[][] terrain;
    [SerializeField]GameObject[] NPCs;
    
    void Start(){
        var WM = GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>();
        terrain = WM.world.terrain;

        // i refers to biom in Constants
        for (int i = 0; i < 3; i++) {
            Instantiate(NPCs[i], SpawnPoint(i), Quaternion.identity);
        }
        
        
    }

    /// <summary>
    /// Finds a random spawnpoint within a biome
    /// </summary>
    /// <returns>x, y position</returns>
    private Vector2 SpawnPoint(int biome) {
        //Find all points on map of a spesific biome type
        var points = new List<Vector2>();
        
        for (int y = 0; y < terrain.Length; y++) {
            for (int x = 0; x < terrain.Length; x++) {
                if (terrain[y][x] == biome) {
                    points.Add(new Vector2(x, terrain.Length - y));
                }
            }
        }
 
        //Choose a random spawnpoint for the npc from the list of points
        return points[UnityEngine.Random.Range(0, points.Count)];
        
    }

}