using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Recipe",menuName = "Recipe")]
public class Recipe: ScriptableObject {
	public int resultingAmount;
	public RecipeEntity[] recipeEntities;
}

[Serializable]
public class RecipeEntity {
	public Entity entity;
	public int amount;
}