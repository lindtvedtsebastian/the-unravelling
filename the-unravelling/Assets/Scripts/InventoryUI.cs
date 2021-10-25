using System;
using UnityEngine;
public class InventoryUI : MonoBehaviour {
        public Transform itemPanel;
        public Transform craftingPanel;
        
        InventoryManager inventory;

        ItemSlot[] itemSlots;
        CraftingSlot[] craftingSlots;

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

        void UpdateUI()
        {
                //Debug.Log("UPDATING UI");

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
