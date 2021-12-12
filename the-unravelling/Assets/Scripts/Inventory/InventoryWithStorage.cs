using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWithStorage : MonoBehaviour {

    public Inventory player;

    public Inventory storage;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        storage = gameObject.AddComponent<Inventory>() as Inventory;
    }

    public void TransferFromStorage(Item item) {
        if(storage.remove(1, item)) player.Add(item);
    }

    public void TransferToStorage(Item item) {
        if(player.remove(1, item)) storage.Add(item);
    }
}
