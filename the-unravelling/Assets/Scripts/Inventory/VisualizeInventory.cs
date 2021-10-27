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

    private ItemSlot[] itemSlots;

    void Start()
    {
        itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();

        updateInventory();
    }
    
    public void ActivateInventory()
    {
        updateInventory();
        inventoryCanvas.SetActive(true);
    }

    public void DeActivateInventory()
    {
        inventoryCanvas.SetActive(false);
    }
    
    private void updateInventory()
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