using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing an inventory
/// </summary>
[Serializable]
public class Inventory : MonoBehaviour {
    public List<Item> items;
    public List<Craft> craft;

    /// <summary>
    /// Constructs a new Inventory 
    /// </summary>
    /// <returns>The new inventory object</returns>
    public Inventory() {
        items = new List<Item>();
        craft = new List<Craft>();
    }

    /// <summary>
    /// Adds or updates a item in the inventory list 
    /// </summary>
    /// <param name="newItem">The item to be added</param>
    public void Add(Item newItem) {
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
    /// <returns>Whether or not the item exists</returns>
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


    /// <summary>
    /// Determines if a recipe can be crafted with the contents of the inventory.
    /// If the recipe can be crafted, the amount will be returned, if not -1 will be returned
    /// </summary>
    /// <param name="recipe">The recipe to be checked against the inventory</param>
    /// <returns>How many times a recipe can be crafted, or -1 if not</returns>
    public int CalculateRecipeCraftingAmount(CraftingRecipe recipe) {
        int currentLowestCraftingAmount = int.MaxValue;
        foreach (RecipeData data in recipe.recipeItems) {
            int itemIndex = findItemDataIndex(data.item);
			
			// If item does not exist in inventory, return
			if (itemIndex < 0) return -1; 
			
            float invAmount = (float) items[itemIndex].amount;
            int currentCraftingAmount = (int) Math.Floor(invAmount / data.amount);

			if (currentCraftingAmount < currentLowestCraftingAmount) {
                currentLowestCraftingAmount = currentCraftingAmount;
            }
        }
		if (currentLowestCraftingAmount != int.MaxValue) {
            return currentLowestCraftingAmount;
        } else return -1;
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

[Serializable]
public class Craft {
    public CraftingRecipe craftingRecipe;
    public int amount;
}
