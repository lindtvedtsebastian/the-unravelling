using System;
using System.Collections.Generic;

/// <summary>
/// A child class Inventory to handle the crafting inventory
/// </summary>
public class InventoryWithCrafting : Inventory {
	public List<RecipeCraftCount> _craftCounts;

	void Start() {
		_craftCounts = new List<RecipeCraftCount>();
		foreach (var recipe in GameData.Get.recipes) {
			_craftCounts.Add(new RecipeCraftCount(recipe,0));
		}
	}

    /// <summary>
    /// Determines if a recipe can be crafted with the contents of the inventory.
    /// If the recipe can be crafted, the amount will be returned, if not -1 will be returned
    /// </summary>
    /// <param name="recipe">The recipe to be checked against the inventory</param>
    /// <returns>How many times a recipe can be crafted, or -1 if not</returns>
    public int CalculateRecipeCraftingAmount(Recipe recipe) {
        int currentLowestCraftingAmount = int.MaxValue;
        foreach (RecipeEntity data in recipe.recipeEntities) {
            int itemIndex = base.findItemDataIndex(data.entity);
			
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

    /// <summary>
    /// Substract the required items for the recipe in the inventory
    /// </summary>
    /// <param name="recipe">The recipe data being substracted</param>
	public void SubstractRecipeFromInventory(Recipe recipe) {
		foreach (RecipeEntity data in recipe.recipeEntities) {
            items[findItemDataIndex(data.entity)].amount -= data.amount;
        }
	}
}

/// <summary>
/// A class for recipes
/// </summary>
public class RecipeCraftCount {
	public Recipe recipe;
	public int amount;

	public RecipeCraftCount(Recipe recipe, int amount) {
		this.recipe = recipe;
		this.amount = amount;
	}
}