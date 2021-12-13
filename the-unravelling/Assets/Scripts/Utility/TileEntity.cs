using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[Serializable]
[CreateAssetMenu(fileName = "TileEntity", menuName = "Entity/TileEntity")]
public class TileEntity : Entity {
	// Color of the entity in the world preview.
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

	protected World getWorld() {
		return GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
	}
}