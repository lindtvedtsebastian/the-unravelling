using UnityEngine;
using System.Linq;


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

    private BaseUnit _previewBaseUnit;
    private SpriteRenderer _previewSprite;

    private Item previewItem;
    private TMPro.TextMeshProUGUI previewAmount;

    private World _world;

    private bool _canRotateSprite = false;
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
        
        _previewSprite = previewCraft.GetComponent<SpriteRenderer>();
        _previewSprite.sprite = item.item.preview;

        previewCraft.GetComponent<PreviewData>().toBePlaced = item;

        previewItem = item;

        _previewBaseUnit = previewItem.item.manifestation.GetComponent<BaseUnit>();

        previewAmount.text = item.amount.ToString();  

        _canRotateSprite = true;

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

    public void RotateSprite() {
        if(!_canRotateSprite) return;

        if(Constants.WALLS.Contains(previewItem.item.id)) {
            _previewBaseUnit.NextSprite(_previewSprite);
        } else if(Constants.GATES.Contains(previewItem.item.id)) {
            previewItem.item.manifestation.transform.GetChild(0).GetComponent<BaseUnit>().NextSprite(_previewSprite);
        }
    }
    
    /// <summary>
    /// Function to place a craft object
    /// </summary>
    public void PlaceObject() {
        if (!previewCraft.activeSelf) return;

        var item = (ComponentEntity) previewCraft.GetComponent<PreviewData>().toBePlaced.item;

        // Check item ID - this makes the rotation of sprites possible since gates have the sprite
        // one level deeper than walls.
        if(Constants.GATES.Contains(previewItem.item.id)) {
            item.manifestation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _previewSprite.sprite;
        } else if (Constants.WALLS.Contains(previewItem.item.id)){
            item.manifestation.GetComponent<SpriteRenderer>().sprite = _previewSprite.sprite;
        }

        Instantiate(item.manifestation, previewCraft.transform.position, Quaternion.identity);

        int y = _world.size - Mathf.FloorToInt(previewCraft.transform.position.y);
        int x = Mathf.FloorToInt(previewCraft.transform.position.x);

        _world.entities[y][x] = previewItem.item.id;

        previewItem.amount -= 1;
        previewAmount.text = previewItem.amount.ToString();

        if(previewItem.amount < 1) {
            previewCraft.SetActive(false);
            _canRotateSprite = false;
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
    /// Activates the inventory. Uses several other functions to ensure that the content
    /// is updates and other actions are cancelled.
    /// </summary>
    public void ActivateInventory() {
        AddItems();
        AddCrafting();
        CancelCraftingHover();
        CancelInventoryAction();
        inventoryCanvas.SetActive(true);
    }

    /// <summary>
    /// Function to de-activate the inventory
    /// </summary>
    public void DeactivateInventory() {
        inventoryCanvas.SetActive(false);
    }
    
    public void RefreshInventory() {
        DeactivateInventory();
        ActivateInventory();
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
                
                if(Constants.WALLS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    craftingSlots[i].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.GATES.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    craftingSlots[i + 6].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.TURRETS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    craftingSlots[i + 12].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.CHESTS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    craftingSlots[i + 18].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.LASERS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    craftingSlots[i + 24].AddCraftingItem(playerInventory._craftCounts[i]);
                }
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
