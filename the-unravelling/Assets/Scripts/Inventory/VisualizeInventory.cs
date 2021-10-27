using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VisualizeInventory : MonoBehaviour {
    [SerializeField]
    private GameObject inventoryCanvas;

    [SerializeField]
    private Inventory playerInventory;

    public Transform itemPanel;
    public Transform craftingPanel;

    private ItemSlot[] itemSlots;
    private CraftingSlot[] craftingSlots;

    void Start()
    {
        itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
        craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();
        updateItems();
    }
    
    public void ActivateInventory()
    {
        updateItems();
        addCrafting();
        inventoryCanvas.SetActive(true);
    }

    public void DeActivateInventory()
    {
        inventoryCanvas.SetActive(false);
    }

    private void addCrafting()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (i < playerInventory.craft.Count)
            {
                craftingSlots[i].AddCraftingItem(playerInventory.craft[i]);
            }
        }
    }

    private void updateItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < playerInventory.items.Count)
            {
                itemSlots[i].AddItem(playerInventory.items[i]);
            }
        }
    }
}

/*public void OnClose(InputAction.CallbackContext ctx)
    {
        DeActivateInventory();
    }*/
/*private void displayList(List<Item> list)
{
    foreach (var i in list)
    {
        Debug.Log(i.item.itemName);
    }
}*/
       
//Debug.Log("Visualize inventory activate");
//displayList(playerInventory.items);
//Debug.Log("Slot count : " + itemSlots.Length);

//Debug.Log("Visualize inventory deactivate");

//Debug.Log(playerInventory.craft[0].craftingRecipe.recipeName);