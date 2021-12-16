using UnityEngine;
using System.Linq;


/// <summary>
/// A class representing the player inventory
/// </summary>
public class PlayerInventoryDisplay : MonoBehaviour {


    [SerializeField]
    public InventoryWithCrafting playerInventory;
    
    [SerializeField] 
    public PlayerBehaviour player;

    public Transform itemPanel;
    public Transform craftingPanel;

    [SerializeField]
    private GameObject _inventoryCanvas;

    public GameObject previewCraft;

    private ItemSlot[] _itemSlots;
    private CraftingSlot[] _craftingSlots;

    private BaseUnit _previewBaseUnit;
    private SpriteRenderer _previewSprite;

    private Item _previewItem;
    private TMPro.TextMeshProUGUI _previewAmount;

    private World _world;

    private bool _canRotateSprite = false;
    void Start() {
        _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
        _itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
		_craftingSlots = craftingPanel.GetComponentsInChildren<CraftingSlot>();

        // Instantiate the preview object that is used for placement preview and the amount that can be placed
        previewCraft = Instantiate(previewCraft);
        _previewAmount = previewCraft.transform.GetChild(0).transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();

        // Run these functions to add content to the inventory on start
        AddItems();
        AddCrafting();
    }
    
    private void Update() {
        MousePlacementPosition();
    }
    
    /// <summary>
    /// Create preview of object to be placed on the map. Sets the sprite of the object to be placed as the preview.
    /// Runs from the input controller class. Other relevant variables are assigned to make rotating of walls and
    /// gates possible. Closes the inventory at the end.
    /// </summary>
    /// <param name="item">A item object in the Item array to create preview from</param>
    /// <see cref="publicCloseInventory()"/>
    public void CreatePreview(in Item item) {
        previewCraft.SetActive(true);
        
        _previewSprite = previewCraft.GetComponent<SpriteRenderer>();
        _previewSprite.sprite = item.item.preview;

        previewCraft.GetComponent<PreviewData>().toBePlaced = item;

        _previewItem = item;

        _previewBaseUnit = _previewItem.item.manifestation.GetComponent<BaseUnit>();

        _previewAmount.text = item.amount.ToString();  

        _canRotateSprite = true;

        player.GetComponent<InputController>().publicCloseInventory();
    }

    /// <summary>
    /// Cancel all hovering in the inventory. Used when exiting the inventory to ensure no 
    /// </summary>
    public void CancelCraftingHover() {
        for (int i = 0; i < _craftingSlots.Length; i++) {
            _craftingSlots[i].craftInfo.SetActive(false);
        }
    }

    /// <summary>
    /// Rotate the sprite. Works on walls and gates where it loops through 3 sprites on input.
    /// The function is listening only when _canRotateSprite is true, which is between createpreview
    /// and placeobject. It also checks to see it the selected object is a wall or gate. 
    /// </summary>
    /// <see cref="NextSprite()"/>
    public void RotateSprite() {
        if(!_canRotateSprite) return;

        if(Constants.WALLS.Contains(_previewItem.item.id)) {
            _previewBaseUnit.NextSprite(_previewSprite);
        } else if(Constants.GATES.Contains(_previewItem.item.id)) {
            _previewItem.item.manifestation.transform.GetChild(0).GetComponent<BaseUnit>().NextSprite(_previewSprite);
        }
    }
    
    /// <summary>
    /// Placement of object on the map. Checks is the preview is active, and instantiates when placed on the map.
    /// Runs until the number of objects to be placed in zero or player presses ESC.
    /// </summary>
    public void PlaceObject() {
        if (!previewCraft.activeSelf) return;

        var item = (ComponentEntity) previewCraft.GetComponent<PreviewData>().toBePlaced.item;

        // Check item ID - this makes the rotation of sprites possible since gates have the sprite
        // one level deeper than walls.
        if(Constants.GATES.Contains(_previewItem.item.id)) {
            item.manifestation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _previewSprite.sprite;
        } else if (Constants.WALLS.Contains(_previewItem.item.id)){
            item.manifestation.GetComponent<SpriteRenderer>().sprite = _previewSprite.sprite;
        }

        Instantiate(item.manifestation, previewCraft.transform.position, Quaternion.identity);

        int y = _world.size - Mathf.FloorToInt(previewCraft.transform.position.y);
        int x = Mathf.FloorToInt(previewCraft.transform.position.x);

        // Update the world entity array to reflect where the item is placed
        _world.entities[y][x] = _previewItem.item.id;

        _previewItem.amount -= 1;
        _previewAmount.text = _previewItem.amount.ToString();

        if(_previewItem.amount < 1) {
            previewCraft.SetActive(false);
            _canRotateSprite = false;
        }
    }

    /// <summary>
    /// Catch the mouse position. Used for placement of object
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
    /// <see cref="AddItems()"/>
    /// <see cref="AddCrafting()"/>
    /// <see cref="CancelCraftingHover()"/>
    /// <see cref="CancelPreviewAction()"/>
    /// </summary>
    public void ActivateInventory() {
        AddItems();
        AddCrafting();
        CancelCraftingHover();
        CancelPreviewAction();
        _inventoryCanvas.SetActive(true);
    }

    /// <summary>
    /// Deactivate the inventory
    /// </summary>
    public void DeactivateInventory() {
        _inventoryCanvas.SetActive(false);
    }

    /// <summary>
    /// Refresh the inventory
    /// </summary>
    public void RefreshInventory() {
        DeactivateInventory();
        ActivateInventory();
    }

    /// <summary>
    /// Cancel any preview action. Runs when inventory is opened to ensure that preview is cancelled.
    /// </summary>
    public void CancelPreviewAction() {
        if (previewCraft.activeSelf) {
            previewCraft.SetActive(false);
        }
    }

    /// <summary>
    /// Add crafting objects to the inventory. Checks if a crafting object can be crafted, and adds the amount
    /// to the specific item that can be placed on the map.
    /// </summary>
    /// <see cref="CalculateRecipeCraftingAmount()"/>
    /// <see cref="AddCraftingItem()"/>
    private void AddCrafting() {
        for (int i = 0; i < _craftingSlots.Length; i++) {
            if (i < playerInventory._craftCounts.Count) {
                playerInventory._craftCounts[i].amount =
                    playerInventory.CalculateRecipeCraftingAmount(playerInventory._craftCounts[i].recipe);
                
                // Check type of crafting object. Each row is of one type
                if(Constants.WALLS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    _craftingSlots[i].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.GATES.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    _craftingSlots[i + 6].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.TURRETS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    _craftingSlots[i + 12].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.CHESTS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    _craftingSlots[i + 18].AddCraftingItem(playerInventory._craftCounts[i]);
                } else if(Constants.LASERS.Contains(playerInventory._craftCounts[i].recipe.resultingEntityID)) {
                    _craftingSlots[i + 24].AddCraftingItem(playerInventory._craftCounts[i]);
                }
            }
        }
    }

    /// <summary>
    /// Add items to the inventory. Removes empty items first, clear the data from the slot, and then adds 
    /// from the item list to the item slots.
    /// </summary>
    /// <see cref="removeEmpty()"/>
    /// <see cref="ClearData()"/>
    /// <see cref="AddItem()"/>
    public void AddItems() {
        playerInventory.removeEmpty();

        for (int i = 0; i < _itemSlots.Length; i++) {
            _itemSlots[i].ClearData();
            if (i < playerInventory.items.Count) {
				_itemSlots[i].AddItem(playerInventory.items[i]);
			}
		}
    }
}

