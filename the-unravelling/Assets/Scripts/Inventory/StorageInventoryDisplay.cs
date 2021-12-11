using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventoryDisplay : MonoBehaviour {
    public GameObject chestInventoryCanvas;

    public Transform _playerPanel;
    public Transform _chestPanel;

    private ItemSlot[] itemSlots;
    private ChestSlot[] chestSlots;

    void Awake() {
        itemSlots = _playerPanel.GetComponentsInChildren<ItemSlot>();
		chestSlots = _chestPanel.GetComponentsInChildren<ChestSlot>();
    }

    public void ActivateChestInventory(InventoryWithChest storage) {
        chestInventoryCanvas.SetActive(true);
    }

    public void DeactivateChestInventory() {
        chestInventoryCanvas.SetActive(false);
    }
}
