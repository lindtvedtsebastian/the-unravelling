using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWithStorage : Inventory {
    public List<Item> chestItems;
    
    public InventoryWithStorage() {
        chestItems = new List<Item>();
    }

    public void TranserFromStorage(int transferAmount, Item item) {
        base.remove(1, item, chestItems);
        base.Add(item);
    }

    public void TransferToStorage(int transferAmount, Item item) {
        base.remove(transferAmount, item);
        base.Add(item, chestItems);
    }
}
