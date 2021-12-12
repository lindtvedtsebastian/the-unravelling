using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWithCrafting : Inventory {

    public List<Craft> craft;
    
    public InventoryWithCrafting() {
        craft = new List<Craft>();

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
            int itemIndex = base.findItemDataIndex(data.item);
			
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

	public void SubstractRecipeFromInventory(CraftingRecipe recipe) {
		foreach (RecipeData data in recipe.recipeItems) {
            items[findItemDataIndex(data.item)].amount -= data.amount;
        }
	}
}

/// <summary>
/// Craft object in the context of an inventory 
/// </summary>
[Serializable]
public class Craft {
    public CraftingRecipe craftingRecipe;
    public int amount;
}
