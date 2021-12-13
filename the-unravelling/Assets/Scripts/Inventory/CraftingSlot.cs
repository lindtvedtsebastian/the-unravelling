using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// A class representing the crafting object slot in the inventory
/// </summary>
public class CraftingSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    private Recipe _recipe;
    public Image craftingImg;
    public Image deactivateImg;
    public Text craftingNum;
    public GameObject craftInfo;
    public Text craftName;
    public Transform craftDisplay;
    public GameObject craftData;
    public PlayerInventoryDisplay playerInventory;
    private Sprite preview;
    private bool hasRecipeDataBeenGenerated = false;

    /// <summary>
    /// Function to add a craft object to the inventory
    /// </summary>
    /// <param name="newCraft">Craft item passed to be added</param>
    public void AddCraftingItem(Recipe recipe) {
        _recipe = recipe;

        if (_recipe.resultingAmount < 1) {
            deactivateImg.sprite = _recipe.resultPreview;
            deactivateImg.enabled = true;
            craftingImg.enabled = false;
            craftingNum.enabled = false;
        }
        else {
            craftingImg.sprite = _recipe.resultPreview;
            deactivateImg.enabled = false;
            craftingImg.enabled = true;
            craftingNum.enabled = true;
            craftingNum.text = _recipe.resultingAmount.ToString();
        }

        preview = _recipe.resultPreview;
        
        if(!hasRecipeDataBeenGenerated) {
            GenerateRecipeData();
            hasRecipeDataBeenGenerated = true;
        }
    }
    
    /// <summary>
    /// Function from the IPointerClickHandler to add a craft object to the inventory side
    /// </summary>
    /// <param name="eventData">Even handler for the point click</param>
    public void OnPointerClick(PointerEventData eventData) {
        if(_recipe != null && _recipe.resultingAmount > 0) {
            //playerInventory.CreatePreview(craft);
            craftInfo.SetActive(false);

            var entity = (ComponentEntity) GameData.Get.worldEntities[_recipe.resultingEntityID];

            // Create a new item object
            Item item = new Item(entity,_recipe.resultingAmount);

            // Add it to the player inventory
            playerInventory.playerInventory.Add(item);

            // Subtract the item cost of the crafting the object
            playerInventory.playerInventory.SubstractRecipeFromInventory(_recipe);

            // Refresh the inventory by closing and opening
            playerInventory.player.GetComponent<InputController>().publicCloseInventory();
            playerInventory.player.GetComponent<InputController>().publicOpenInventory();
        }
    }

    /// <summary>
    /// Function from the IPointerClickHandler to grab hovering over this object
    /// </summary>
    /// <param name="eventData">Even handler for the point click</param>
    public void OnPointerEnter(PointerEventData eventData) {
        if(_recipe == null) return;

        craftName.text = _recipe.name;
        craftInfo.SetActive(true);
    }

    /// <summary>
    /// Function from the IPointerClickHandler to grab exit hovering from an object
    /// </summary>
    /// <param name="eventData">Even handler for the point click</param>
    public void OnPointerExit(PointerEventData eventData) {
        if(_recipe != null) {
            craftInfo.SetActive(false);
        }
    }

    /// <summary>
    /// Function to generate the data required for crafting this object. Loops through the length
    /// of the list of required items. Instantiates a gameobject and inserts the correct data required
    /// for crafting.
    /// </summary>
    /// <param name="Craft">the craft object to be checked</param>
    public void GenerateRecipeData() {
        for (int i = 0; i < _recipe.recipeEntities.Length; i++) {
            GameObject hoverData = Instantiate(craftData, craftDisplay.transform, true);

            hoverData.transform.GetChild(0).GetComponent<Image>().sprite = _recipe.recipeEntities[i].entity.preview;
            hoverData.transform.GetChild(1).GetComponent<Text>().text = _recipe.recipeEntities[i].amount.ToString();
            hoverData.transform.GetChild(2).GetComponent<Text>().text = "X";
            hoverData.transform.GetChild(3).GetComponent<Text>().text = _recipe.recipeEntities[i].entity.name;

            hoverData.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}