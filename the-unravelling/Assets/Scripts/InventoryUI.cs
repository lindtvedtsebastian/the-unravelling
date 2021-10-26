using System;
using UnityEngine;

public delegate void onClickCraftingItem(in ItemData craftingItem);

public class InventoryUI : MonoBehaviour {
        public Transform itemPanel;
        public Transform craftingPanel;
        
        InventoryManager inventory;

        public InventoryItem item;

        ItemSlot[] itemSlots;
        CraftingSlot[] craftingSlots;
        
        private onClickCraftingItem callback;

        void Start()
        {
                inventory = FindObjectOfType<InventoryManager>();
                inventory.onItemChangedCallback += UpdateUI;

                itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
                craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();
        }

        private void Update()
        {
                
        }
        
        public void OnActivateInventory(InventoryUI inventory, onClickCraftingItem click) {
                callback = click;
                
                Debug.Log(callback);

                // Activate the UI
                gameObject.SetActive(true);

                // Create previews for all the items in the inventory
                //foreach (var item in inventory.GetItems()) {
                //var cell = Instantiate(itemPrefab, panel.transform, true);
                //cell.AddItemData(item, CloseInventory);
                //}
        }

        void UpdateUI()
        { 
                for (int i = 0; i < itemSlots.Length; i++)
                {
                        if (i < inventory.items.Count)
                        {
                                itemSlots[i].AddItem(inventory.items[i]);
                        }
                }

                for (int i = 0; i < craftingSlots.Length; i++)
                {
                        if (i < inventory.craftItems.Count)
                        {
                                Debug.Log("Adding crafting item");
                                craftingSlots[i].AddCraftingItem(inventory.craftItems[i]);
                        }
                }
        }
}
