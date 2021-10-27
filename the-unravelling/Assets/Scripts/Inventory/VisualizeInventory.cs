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

    public Item stone;
    public Item wood;

    void Start()
    {
        itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
        
        //playerInventory.Add(stone);
        //playerInventory.Add(wood);
        
        updateInventory();
    }
    
    public void ActivateInventory()
    {
        updateInventory();
        Debug.Log("Visualize inventory activate");
        displayList(playerInventory.items);
        Debug.Log("Slot count : " + itemSlots.Length);
        inventoryCanvas.SetActive(true);
    }

    public void DeActivateInventory()
    {
        Debug.Log("Visualize inventory deactivate");
        inventoryCanvas.SetActive(false);
    }

    private void displayList(List<Item> list)
    {
        foreach (var i in list)
        {
            Debug.Log(i.item.itemName);
        }
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

    /*public void OnClose(InputAction.CallbackContext ctx)
    {
        DeActivateInventory();
    }*/
}
