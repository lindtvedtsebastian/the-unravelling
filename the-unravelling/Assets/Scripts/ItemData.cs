using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ItemData is a generic class for storing information about an item.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory and Items/ItemData", order = 2)]
public class ItemData : ScriptableObject {
    // Name of the item
    public string itemName;

    // Preview sprite for the item, useful for inventories etc
    public Sprite preview;

    // The prefab representing the real physical manifestation of the item
    public GameObject manifestation;

    // TODO: Add reference to the game object that can be made from this item, if any
    // TODO: Some kind of generic stats or something
}
