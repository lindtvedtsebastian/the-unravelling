using UnityEngine;

/// <summary>
/// A class for storage inventory
/// </summary>
public class InventoryWithStorage : MonoBehaviour {

    public Inventory player;

    public Inventory storage;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        storage = gameObject.AddComponent<Inventory>() as Inventory;
    }

    /// <summary>
    /// Transfer item from chest storage to player inventory.
    /// </summary>
    public void TransferFromStorage(Item item) {
        if(storage.remove(1, item)) player.Add(item);
    }

    /// <summary>
    /// Transfer item from player inventory to chest storage
    /// </summary>
    public void TransferToStorage(Item item) {
        if(player.remove(1, item)) storage.Add(item);
    }
}
