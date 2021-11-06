using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
	public static IWorld generateWorld(int size = 256) {
        IWorld world = new IWorld();
        world.size = size;
        world.terrain = createJagged2dArray(size,size);

        return world;
    }

    /// <summary>
    /// Creates a NxM jagged int array 
    /// </summary>
    /// <param name="width">The width of the array</param>
    /// <param name="height">The height of the array</param>
    /// <returns>The new jagged 2d array</returns>
    public static int[][] createJagged2dArray(int width, int height) {
        int[][] array = new int[height][];
        for (int i = 0; i < array.Length; i++) {
            array[i] = new int[width];
        }
        return array;
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

