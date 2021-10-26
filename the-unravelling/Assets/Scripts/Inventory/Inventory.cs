using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory : MonoBehaviour {
    public List<Item> items;

	public Inventory() {
        items = new List<Item>();
    }
}

[Serializable]
public class Item {
    public ItemData item;
    public int amount;
}
