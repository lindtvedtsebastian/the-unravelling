using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing an inventory
/// </summary>
[Serializable]
public class Inventory : MonoBehaviour {
    public List<Item> items;

    /// <summary>
    /// Constructs a new Inventory 
    /// </summary>
    /// <returns>The new inventory object</returns>
    public Inventory() {
        items = new List<Item>();
    }

    /// <summary>
    /// Adds or updates a item in the inventory list 
    /// </summary>
    /// <param name="newItem">The item to be added</param>
    void Add(Item newItem) {
		if (!checkIfItemExists(newItem.item)) {
            items.Add(newItem);
        } else {
            items[findItemDataIndex(newItem.item)].amount += newItem.amount;
        }
	}

    /// <summary>
    /// Checks if a given itemData exists in the inventory list
    /// </summary>
    /// <param name="itemData">The itemdata to check against the inv list</param>
    /// <returns>Wheter or not the item exists</returns>
    private bool checkIfItemExists(ItemData itemData) {
		foreach (Item item in items) {
			if (item.item == itemData) {
                return true;
            }
		}
		return false; 
	}

    /// <summary>
    /// Finds the index of the itemData in the inventory list 
    /// </summary>
    /// <param name="itemData">The itemData to find the index of</param>
    /// <returns>The index if found, -1 otherwise</returns>
    private int findItemDataIndex(ItemData itemData) {
        for (int i = 0; i < items.Count; i++) {
			if (items[i].item == itemData) {
                return i; 
            }
		}
        return -1;
    }

}

/// <summary>
/// Item in the context of an inventory 
/// </summary>
[Serializable]
public class Item {
    public ItemData item;
    public int amount;
}
