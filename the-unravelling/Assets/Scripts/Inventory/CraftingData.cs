using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Crafting Recipes", menuName = "Crafting/Crafting Recipes", order = 2)]
public class CraftingRecipes : ScriptableObjectSingleton<CraftingRecipes> {
    public CraftingRecipe[] craftingRecipes;
}

[CreateAssetMenu(fileName = "Crafting recipe", menuName = "Crafting/Crafting recipe"]
 public class CraftingRecipe : ScriptableObject {
    public int id;
    public string recipeName;
    public int resultingAmount;
    public RecipeData[] recipeItems; 
}

 [Serializable]
 public class RecipeData {
    public ItemData item;
    public int amount; 
}
