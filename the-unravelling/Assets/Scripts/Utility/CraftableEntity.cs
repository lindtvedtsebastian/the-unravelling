using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CraftableEntity", menuName = "Entity/CraftableEntity")]
public class CraftableEntity : IWorldEntity {
	public Sprite preview;
	public Recipe recipe;
}