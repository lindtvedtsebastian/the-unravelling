using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

/// <summary>
/// A Item that can be spawned into and rendered as part of the world in a tilemap.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "World Entity", menuName = "World Entities/World Entity")]
public class WorldEntity : ItemData {
	// Color of the preview tile on a map preview.
	public Color mapColor;

	// Set of tiles that can represent this world entity.
	// The world renderer will pick one of these to render as the entity.
	public TileBase[] tiles;

	/// <summary>
	/// Base function for setting a tile sprite at a world coordinate [x,y]
	/// </summary>
	/// <param name="y">The tile's position on the y axis</param>
	/// <param name="x">The tile's position on the x axis</param>
	/// <returns></returns>
	public virtual TileBase SetSprite(int y, int x) {
		return tiles[Random.Range(0, tiles.Length)];
	}

}