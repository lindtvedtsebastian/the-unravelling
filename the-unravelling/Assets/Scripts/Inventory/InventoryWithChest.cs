using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWithChest : Inventory {
    public List<Item> chestItems;

    public InventoryWithChest() {
        chestItems = new List<Item>();
    }

    public void TranserFromStorage(Item item) {
        base.remove(item, chestItems);
        base.Add(item);
    }

    public void TransferToStorage(Item item) {
        base.remove(item);
        base.Add(item, chestItems);
    }
}
