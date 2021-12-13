
using System;
using UnityEngine;

[Serializable]
public class Entity : ScriptableObject {
	// Numeric ID that uniquely identifies this item.
	public int id;

	// Name of the item.
	public string entityName;

	// Color of the entity in the world preview.
	public Color mapColor;
}
