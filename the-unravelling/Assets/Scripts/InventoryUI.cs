using System;
using UnityEngine;
public class InventoryUI : MonoBehaviour {

        public Transform itemPanel;
        
        InventoryManager inventory;

        ItemSlot[] itemSlots;

        void Start()
        {
                inventory = FindObjectOfType<InventoryManager>();
                inventory.onItemChangedCallback += UpdateUI;

                itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
        }

        private void Update()
        {
                
        }

        void UpdateUI()
        {
                Debug.Log("UPDATING UI");

                for (int i = 0; i < itemSlots.Length; i++)
                {
                        if (i < inventory.items.Count)
                        {
                                itemSlots[i].AddItem(inventory.items[i]);
                        }
                }
        }
}
