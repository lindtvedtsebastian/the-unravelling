using System;
using UnityEngine;

/// <summary>
/// Type of item represented by ItemData object.
/// </summary>
public enum ItemType {
    GameObject,
    Tile,
}

/// <summary>
/// ItemData is a generic class for storing information about an item.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData", order = 2)]
public class ItemData : ScriptableObject {
    // Numeric ID that uniquely identifies this item.
    public int id;
    
    // Name of the item.
    public string itemName;

    public int itemAmount = 1;

    // Type of the item.
    public ItemType type;

    // Preview sprite for the item, useful for inventories etc.
    public Sprite preview;

    // The prefab representing the real physical manifestation of the item.
    public GameObject manifestation;

	// The world object
	private World _world;

	protected World getWorld() {
		if (_world.size == 0) {
			_world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
		}
		return _world;
	}
}
