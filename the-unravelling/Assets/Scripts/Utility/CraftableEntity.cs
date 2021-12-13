using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CraftableEntity", menuName = "Entity/CraftableEntity")]
public class CraftableEntity : WorldEntity {
	public Recipe recipe;
}