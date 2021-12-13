using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Recipe",menuName = "Recipe")]
public class Recipe: ScriptableObject {
	public int resultingEntityID;
	public Sprite resultPreview;
	public int resultingAmount;
	public RecipeEntity[] recipeEntities;
}

[Serializable]
public class RecipeEntity {
	public ComponentEntity entity;
	public int amount;
}