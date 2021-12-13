using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "IWorldEntity", menuName = "Entity/IWorldEntity")]
public class IWorldEntity : Entity {
	public GameObject manifestation;
	public Drop[] drops;
}

[Serializable]
public class Drop {
	public GameObject dropObject;
	public int amount;
}