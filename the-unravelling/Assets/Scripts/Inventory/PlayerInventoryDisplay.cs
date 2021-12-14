using UnityEngine;


/// <summary>
/// A class representing the player inventory
/// </summary>
public class PlayerInventoryDisplay : MonoBehaviour {
    [SerializeField]
    private GameObject inventoryCanvas;

    [SerializeField]
    public InventoryWithCrafting playerInventory;
    
    [SerializeField] 
    public PlayerBehaviour player;

    public Transform itemPanel;
    public Transform craftingPanel;

    private ItemSlot[] itemSlots;
    private CraftingSlot[] craftingSlots;

    public GameObject previewCraft;

    private Item previewItem;
    private TMPro.TextMeshProUGUI previewAmount;

    private World _world;
    void Start() {
        _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
        itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
		craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();

        previewCraft = Instantiate(previewCraft);

        previewAmount = previewCraft.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        AddItems();
        AddCrafting();
    }
    
    private void Update() {
        MousePlacementPosition();
    }
    
    /// <summary>
    /// Function to create a preview from a craft object
    /// </summary>
    /// <param name="item">A item object in the Item array to create preview from</param>
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

        var item = (ComponentEntity) previewCraft.GetComponent<PreviewData>().toBePlaced.item;
        Instantiate(item.manifestation, previewCraft.transform.position, Quaternion.identity);

        int y = _world.size - Mathf.FloorToInt(previewCraft.transform.position.y);
        int x = Mathf.FloorToInt(previewCraft.transform.position.x);

        _world.entities[y][x] = previewItem.item.id;

        previewItem.amount -= 1;
        previewAmount.text = previewItem.amount.ToString();

        if(previewItem.amount < 1) {
            previewCraft.SetActive(false);
        }
    }

    /// <summary>
    /// Function to grab the mouse position for placing a craft object
    /// </summary>
    public void MousePlacementPosition() {
        if (previewCraft != null && previewCraft.activeSelf) {
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
            if (i < playerInventory._craftCounts.Count) {
                playerInventory._craftCounts[i].amount =
                    playerInventory.CalculateRecipeCraftingAmount(playerInventory._craftCounts[i].recipe);
                
                craftingSlots[i].AddCraftingItem(playerInventory._craftCounts[i]);
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
