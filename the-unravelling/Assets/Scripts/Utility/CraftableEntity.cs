using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CraftableEntity", menuName = "Entity/CraftableEntity")]
public class CraftableEntity : IWorldEntity {
	public Recipe recipe;
}