using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting Recipes", menuName = "Crafting/Crafting Recipes")]
public class CraftingRecipes : ScriptableObjectSingleton<CraftingRecipes> {
    public CraftingRecipe[] craftingRecipes;
}

[Serializable]
[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "Crafting/Crafting recipe")]
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
