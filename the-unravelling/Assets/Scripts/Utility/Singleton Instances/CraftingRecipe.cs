using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipes", menuName = "Crafting/Crafting Recipes")]
public class CraftingRecipes : ScriptableObjectSingleton<CraftingRecipes> {
    public CraftingRecipe[] craftingRecipes;
}

[Serializable]
[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Crafting/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject {
    public int id;
    public string recipeName;
    public Sprite craftPreview;
    public GameObject manifestation;
    public ItemData itemRepresentation;
    public int resultingAmount;
    public RecipeData[] recipeItems; 
}

[Serializable]
public class RecipeData {
    public ItemData item;
    public int amount; 
}
