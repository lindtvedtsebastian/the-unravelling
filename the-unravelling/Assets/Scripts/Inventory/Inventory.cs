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
    public void Add(Item newItem, List<Item> list = null) {
        if(list == null) list = items;

        if (!checkIfItemExists(newItem.item, list)) {
            list.Add(newItem);
        } else {
            list[findItemDataIndex(newItem.item, list)].amount += newItem.amount;
        }
	}

	public void removeEmpty() {
        items.RemoveAll((Item item) => {
            return item.amount <= 0;
        });
    }

    public void remove(int decreaseAmount, Item item, List<Item> list = null) {
        if(list == null) list = items;
        if(list[findItemDataIndex(item.item)].amount >= item.amount) {
            list[findItemDataIndex(item.item)].amount -= decreaseAmount;
        }
    }

    /// <summary>
    /// Checks if a given itemData exists in the inventory list
    /// </summary>
    /// <param name="itemData">The itemdata to check against the inv list</param>
    /// <returns>Whether or not the item exists</returns>
    protected bool checkIfItemExists(ItemData itemData, List<Item> list = null) {
        if(list == null) list = items;
		foreach (Item item in list) {
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
    protected int findItemDataIndex(ItemData itemData, List<Item> list = null) {
        if(list == null) list = items;
        for (int i = 0; i < list.Count; i++) {
			if (list[i].item == itemData) {
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


