using UnityEngine;

[CreateAssetMenu(fileName = "World Data", menuName = "Scriptable Objects/World/World Data")]
public class WorldData : ScriptableObjectSingleton<WorldData> {
	public int mapSize;
	public int[,] map;
	public string mapName;

	public WorldEntity GRASS;
	public WorldEntity DIRT;
	public WorldEntity STONE;
}
