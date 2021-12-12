using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventoryDisplay : MonoBehaviour {
    public GameObject chestInventoryCanvas;
    public Transform _playerPanel;
    public Transform _chestPanel;
    private StoragePlayerItemSlot[] itemSlots;
    private StorageItemSlot[] storageSlots;

    void Awake() {
        itemSlots = _playerPanel.GetComponentsInChildren<StoragePlayerItemSlot>();
		storageSlots = _chestPanel.GetComponentsInChildren<StorageItemSlot>();
    }

    public void ActivateStorageInventory(InventoryWithStorage storage) {
        AddItems(storage);
        chestInventoryCanvas.SetActive(true);
    }

    public void DeactivateStorageInventory() {
        chestInventoryCanvas.SetActive(false);
    }

    public void RefreshStorageInventory(InventoryWithStorage storage) {
        DeactivateStorageInventory();
        ActivateStorageInventory(storage);
    }

    private void AddItems(InventoryWithStorage storage) {
        storage.player.removeEmpty();

        for(int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].ClearData();
            if(i < storage.player.items.Count) {
                itemSlots[i].AddItemStorage(storage.player.items[i], storage);
            }
        }
    }

    /// <summary>
    /// Development function to check the inventory content
    /// </summary>
    /// <param name="storage">The storage to check content</param>
    public void InventoryContent(InventoryWithStorage storage) {
        for (int i = 0; i < storage.player.items.Count; i++) {
            if(storage.player.items[i] == null) return;
            Debug.Log("Item count : " + i + " is -> " + storage.player.items[i].item.itemName + 
                                            " count -> " + storage.player.items[i].amount);
        }
    }
}
