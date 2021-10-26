using System;
using UnityEngine;


public class PlayerInventoryUI : MonoBehaviour {
        public Transform itemPanel;
        public Transform craftingPanel;
        
        PlayerInventory inventory;

        public InventoryItem item;

        ItemSlot[] itemSlots;
        CraftingSlot[] craftingSlots;
        
        public GameObject previewTurret;
        public CraftingData turret;

        void Start()
        {
                inventory = FindObjectOfType<PlayerInventory>();
                inventory.onItemChangedCallback += UpdateUI;

                itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
                craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();
                
                // We need to make a new instance of the game object, so that we can use it.
                previewTurret = Instantiate(previewTurret);
                // But it should still be disabled
                previewTurret.SetActive(false);
        }
        
        /*public void PreviewTurret()
        {
                if (previewTurret.activeSelf) return;
        
                //if (!inventory.HasItem(item)) return;

                previewTurret.SetActive(true);
                var sprite = previewTurret.GetComponent<SpriteRenderer>();
                sprite.sprite = turret.preview;
                
                Debug.Log("Create preview : " + previewTurret);
        }

        public void PlaceTurret()
        {
                // Only place item, if preview was active
                if (!previewTurret.activeSelf) return;
        
                Debug.Log("Placing object");

                // Remove item from inventory
                //if (!inventory.RemoveItem(item)) return;

                // Create final object
                Instantiate(turret.manifestation, previewTurret.transform.position, Quaternion.identity);

                // Deactivate the preview
                previewTurret.SetActive(false);
        }

        public void OnActivateInventory(PlayerInventoryUI inventory, onClickCraftingItem click) {
                callback = click;
                
                Debug.Log(callback);

                // Activate the UI
                gameObject.SetActive(true);

                // Create previews for all the items in the inventory
                //foreach (var item in inventory.GetItems()) {
                //var cell = Instantiate(itemPrefab, panel.transform, true);
                //cell.AddItemData(item, CloseInventory);
                //}
        }*/

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
