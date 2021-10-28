using System;
using System.Collections.Generic;
using Unity.Assertions;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnClickInventory(in Craft craft);

public class VisualizeInventory : MonoBehaviour {
    [SerializeField]
    private GameObject inventoryCanvas;

    [SerializeField]
    private Inventory playerInventory;

    public Transform itemPanel;
    public Transform craftingPanel;

    private ItemSlot[] itemSlots;
    private CraftingSlot[] craftingSlots;
    
    private Mouse mouse;
    private Camera currentCamera;

    //private Sprite preview;

    public GameObject previewCraft;

    private OnClickInventory callback;

    void Start()
    {
        itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
        craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();
        
        mouse = Mouse.current;
        currentCamera = Camera.main;

        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");
        
        updateItems();
    }
    
    private void Update()
    {
        MousePosPlacement();
    }
    
    public void CreatePreview(in Craft craft)
    {
        previewCraft = Instantiate(previewCraft);
        previewCraft.SetActive(true);
        
        var sprite = previewCraft.GetComponent<SpriteRenderer>();
        sprite.sprite = craft.craftingRecipe.craftPreview;
        
        DeActivateInventory();
        //CancelInventoryAction();
    }
    
    private void PlaceObject(in Craft craft) {
        // Only place item, if preview was active
        if (!previewCraft.activeSelf) return;
        
        //Debug.Log("Placing object");

        // Remove item from inventory
        //if (!inventory.RemoveItem(item)) return;

        // Create final object
        Instantiate(craft.craftingRecipe.manifestation, previewCraft.transform.position, Quaternion.identity);

        // Deactivate the preview
        previewCraft.SetActive(false);
    }

    public void MousePosPlacement()
    {
        if (previewCraft.activeSelf) {
            previewCraft.transform.position = GetMousePosition();
            previewCraft.transform.position = new Vector3(
                Mathf.Floor(previewCraft.transform.position.x) + 0.5f,
                Mathf.Floor(previewCraft.transform.position.y) + 0.5f,
                previewCraft.transform.position.z);
        }
    }
    private Vector3 GetMousePosition() {
        // Grab the position of the mouse in screen space
        Vector3 mousePos = mouse.position.ReadValue();
        mousePos.z = 1.0f;

        // Convert to world space coordinates
        return currentCamera.ScreenToWorldPoint(mousePos);
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

    public void CancelInventoryAction()
    {
        foreach (var slot in craftingSlots)
        {
            if (slot.previewCraft.activeSelf) {
                slot.previewCraft.SetActive(false);
            }
        }
    }

    private void addCrafting()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (i < playerInventory.craft.Count)
            {
                playerInventory.craft[i].craftingRecipe.resultingAmount = 
                    playerInventory.CalculateRecipeCraftingAmount(playerInventory.craft[i].craftingRecipe);
                
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