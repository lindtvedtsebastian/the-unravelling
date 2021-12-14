using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "WorldEntity", menuName = "Entity/WorldEntity")]
public class WorldEntity : ComponentEntity {
	public Drop[] drops;
}

[Serializable]
public class Drop {
	public GameObject dropObject;
	public int amount;
}