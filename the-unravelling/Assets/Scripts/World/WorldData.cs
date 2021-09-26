using UnityEngine;

[CreateAssetMenu(fileName = "World Data", menuName = "Scriptable Objects/World/World Data")]
public class WorldData : ScriptableObjectSingleton<WorldData> {
	public int worldSize;
	public int[,] map;
	public string mapName;

    public const int NW = 0b1;
    public const int N  = 0b10;
    public const int NE = 0b100;
    public const int W  = 0b1000;
    public const int E  = 0b10000;
    public const int SW = 0b100000;
    public const int S  = 0b1000000;
    public const int SE = 0b10000000;

    public WorldEntity GRASS;
	public WorldEntity DIRT;
	public WorldEntity STONE;
}
