using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing an inventory
/// </summary>
[Serializable]
public class Inventory : MonoBehaviour {
    public List<Item> items;

    void Start() {
        items = new List<Item>();
    }

    /// <summary>
    /// Adds or updates a item in the inventory list 
    /// </summary>
    /// <param name="newItem">The item to be added</param>
    public void Add(Item newItem) {
        if (!checkIfItemExists(newItem.item)) {
            items.Add(new Item(newItem.item, 1));
        } else {
            items[findItemDataIndex(newItem.item)].amount += 1;
        }
	}

	public void removeEmpty() {
        items.RemoveAll((Item item) => {
            return item.amount <= 0;
        });
    }

    public bool remove(int decreaseAmount, Item item) {
        if(items[findItemDataIndex(item.item)].amount >= 1) {
            items[findItemDataIndex(item.item)].amount -= 1;
            return true;
            
        } else return false;
    }

    /// <summary>
    /// Checks if a given itemData exists in the inventory list
    /// </summary>
    /// <param name="itemData">The itemdata to check against the inv list</param>
    /// <returns>Whether or not the item exists</returns>
    protected bool checkIfItemExists(Entity entity) {
		foreach (Item item in items) {
			if (item.item == entity) {
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
    protected int findItemDataIndex(Entity entity) {
        for (int i = 0; i < items.Count; i++) {
			if (items[i].item == entity) {
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
    public ComponentEntity item;
    public int amount;

    public Item(ComponentEntity item, int amount) {
        this.item = item;
        this.amount = amount;
    }
}


