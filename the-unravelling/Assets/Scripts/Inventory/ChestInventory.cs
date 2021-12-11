using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour {

    [SerializeField]
    private GameObject _chestInventory;

    [SerializeField]
    private GameObject chestInventoryCanvas;

    [SerializeField]
    private Inventory _inventory;

    public List<Item> chestItems;
    private List<Item> playerItems;

    public Transform _playerPanel;
    public Transform _chestPanel;

    private ItemSlot[] itemSlots;
    private ChestSlot[] chestSlots;

    public ChestInventory() {
        chestItems = new List<Item>();
        playerItems = new List<Item>();
    }

    void Awake() {
        itemSlots = _playerPanel.GetComponentsInChildren<ItemSlot>();
		chestSlots = _chestPanel.GetComponentsInChildren<ChestSlot>();
    }

    public void ActivateChestInventory() {
        playerItems = _inventory.items;
        chestInventoryCanvas.SetActive(true);
    }

    public void DeactivateChestInventory() {
        chestInventoryCanvas.SetActive(false);
    }
}
