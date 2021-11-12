using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
	public static IWorld generateWorld(int size = 256, int seed = 123) {
        IWorld world = new IWorld();
        world.size = size;
        float[][] terrainNoise = Noise.generateNoiseMap(seed:seed, offset: new Vector2(0,0));

		
		
        return world;
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

