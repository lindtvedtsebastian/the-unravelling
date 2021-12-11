using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour {
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
        playerItems = _inventory.items;
    }

    public void ActivateChestInventory() {
        chestInventoryCanvas.SetActive(true);
    }

    public void DeactivateChestInventory() {
        chestInventoryCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
