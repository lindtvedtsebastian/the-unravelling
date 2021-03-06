//// <summary>

/// A static class that contains all the various gameIDs for easy access
/// </summary>
public static class Constants {
	public const int GRASS = 0;
	public const int DIRT = 1;
	public const int STONE = 2;

	public const int TREE = 3;
	public const int DRY_TREE = 4;

	public const int STONE_ORE = 5;
	public const int IRON_ORE = 6;
	public const int COPPER_ORE = 7;

	public const int WOOD_WALL = 8;
	public const int STONE_WALL = 9;
	public const int IRON_WALL = 10;
	public const int COPPER_WALL = 11;

	public const int WOOD_GATE = 12;
	public const int STONE_GATE = 13;
	public const int IRON_GATE = 14;
	public const int COPPER_GATE = 15;

	public const int WOOD_TURRET = 16;
	public const int STONE_TURRET = 17;
	public const int IRON_TURRET = 18;
	public const int COPPER_TURRET = 19;

	public const int WOOD_CHEST = 20;
	public const int STONE_CHEST = 21;
	public const int IRON_CHEST = 22;
	public const int COPPER_CHEST = 23;

	public const int WOOD_COMPONENT = 24;
	public const int STONE_COMPONENT = 25;
	public const int IRON_COMPONENT = 26;
	public const int COPPER_COMPONENT = 27;

	public const int WOOD_LASER = 28;

	public const int MAGIC_ORE = 29;
	public const int MAGIC_COMPONENT = 30;

	public static readonly int[] WALLS = {WOOD_WALL, STONE_WALL, IRON_WALL, COPPER_WALL};
	public static readonly int[] GATES = {WOOD_GATE, STONE_GATE, IRON_GATE, COPPER_GATE};
	public static readonly int[] TURRETS = {WOOD_TURRET, STONE_TURRET, IRON_TURRET, COPPER_TURRET};
	public static readonly int[] CHESTS = {WOOD_CHEST, STONE_CHEST, IRON_CHEST, COPPER_CHEST};
	public static readonly int[] LASERS = {WOOD_LASER};

	public const int NW = 0b1;
	public const int N = 0b10;
	public const int NE = 0b100;
	public const int W = 0b1000;
	public const int E = 0b10000;
	public const int SW = 0b100000;
	public const int S = 0b1000000;
	public const int SE = 0b10000000;
}