using System;
using System.Collections.Generic;
using Unity.Assertions;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// A class representing the player inventory
/// </summary>
public class PlayerInventory : MonoBehaviour {
    [SerializeField]
    private GameObject inventoryCanvas;

    [SerializeField]
    public Inventory playerInventory;
    
    [SerializeField] 
    public PlayerBehaviour player;

    public Transform itemPanel;
    public Transform craftingPanel;

    private ItemSlot[] itemSlots;
    private CraftingSlot[] craftingSlots;

    public GameObject previewCraft;

    private Item previewItem;
    private TMPro.TextMeshProUGUI previewAmount;

    void Start() {
        itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
		craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();

        previewCraft = Instantiate(previewCraft);

        previewAmount = previewCraft.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        AddItems();
        AddCrafting();
    }
    
    private void Update() {
        MousePosPlacement();
    }
    
    /// <summary>
    /// Function to create a preview from a craft object
    /// </summary>
    /// <param name="craft">A craft object to create a preview from</param>
    public void CreatePreview(in Item item) {
        previewCraft.SetActive(true);
        
        var sprite = previewCraft.GetComponent<SpriteRenderer>();
        sprite.sprite = item.item.preview;

        previewCraft.GetComponent<PreviewData>().toBePlaced = item;

        previewItem = item;
        previewAmount.text = item.amount.ToString();  

        player.GetComponent<InputController>().publicCloseInventory();
    }

    /// <summary>
    /// Function to cancel all the hovering of crafting objects in inventory
    /// </summary>
    public void CancelCraftingHover() {
        for (int i = 0; i < craftingSlots.Length; i++) {
            craftingSlots[i].craftInfo.SetActive(false);
        }
    }
    
    /// <summary>
    /// Function to place a craft object
    /// </summary>
    public void PlaceObject() {
        if (!previewCraft.activeSelf) return;

        ItemData item = previewCraft.GetComponent<PreviewData>().toBePlaced.item;
        Instantiate(item.manifestation, previewCraft.transform.position, Quaternion.identity);

        previewItem.amount -= 1;
        previewAmount.text = previewItem.amount.ToString();

        if(previewItem.amount < 1) {
            previewCraft.SetActive(false);
        }
    }

    /// <summary>
    /// Function to grab the mouse position for placing a craft object
    /// </summary>
    public void MousePosPlacement() {
        if (previewCraft.activeSelf) {
            previewCraft.transform.position = player.GetComponent<InputController>().GetMousePosition();
            previewCraft.transform.position = new Vector3(
                Mathf.Floor(previewCraft.transform.position.x) + 0.5f,
                Mathf.Floor(previewCraft.transform.position.y) + 0.5f,
                previewCraft.transform.position.z);
        }
    }
    
    /// <summary>
    /// Function to activate the inventory
    /// </summary>
    public void ActivateInventory() {
        AddItems();
        AddCrafting();
        CancelCraftingHover();
        inventoryCanvas.SetActive(true);
    }

    /// <summary>
    /// Development function to check the inventory content
    /// </summary>
    public void InventoryContent() {
        for (int i = 0; i < playerInventory.items.Count; i++) {
            if(playerInventory.items[i] == null) return;
            Debug.Log("Item count : " + i + " is -> " + playerInventory.items[i].item.itemName + 
                                            " count -> " + playerInventory.items[i].amount);
        }
    }

    /// <summary>
    /// Function to de-activate the inventory
    /// </summary>
    public void DeactivateInventory() {
        inventoryCanvas.SetActive(false);
    }

    /// <summary>
    /// Function to cancel an inventory action
    /// </summary>
    public void CancelInventoryAction() {
        if (previewCraft.activeSelf) {
            previewCraft.SetActive(false);
        }
    }

    /// <summary>
    /// Function to add the crafting items to the inventory
    /// </summary>
    private void AddCrafting() {
        for (int i = 0; i < craftingSlots.Length; i++) {
            if (i < playerInventory.craft.Count) {
                playerInventory.craft[i].craftingRecipe.resultingAmount = 
                    playerInventory.CalculateRecipeCraftingAmount(playerInventory.craft[i].craftingRecipe);
                
                craftingSlots[i].AddCraftingItem(playerInventory.craft[i]);
            }
        }
    }

    /// <summary>
    /// Function to update the items in the inventory
    /// </summary>
    public void AddItems() {
        playerInventory.removeEmpty();

        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].ClearData();
            if (i < playerInventory.items.Count) {
				itemSlots[i].AddItem(playerInventory.items[i]);
			}
		}
    }
}
