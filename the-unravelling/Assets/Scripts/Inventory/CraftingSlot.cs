using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// A class representing the crafting object slot in the inventory
/// </summary>
public class CraftingSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    Craft craft;
    public Image craftingImg;
    public Image deactivateImg;
    public Text craftingNum;
    public GameObject craftInfo;
    public Text craftName;
    public Transform craftDisplay;
    public GameObject craftData;
    public PlayerInventory playerInventory;
    private Sprite preview;
    private bool hasRecipeDataBeenGenerated = false;

    /// <summary>
    /// Function to add a craft object to the inventory
    /// </summary>
    /// <param name="newCraft">Craft item passed to be added</param>
    public void AddCraftingItem(Craft newCraft) {
        craft = newCraft;
        
        if (craft.craftingRecipe.resultingAmount < 1) {
            deactivateImg.sprite = craft.craftingRecipe.craftPreview;
            deactivateImg.enabled = true;
            craftingImg.enabled = false;
            craftingNum.enabled = false;
        }
        else {
            craftingImg.sprite = craft.craftingRecipe.craftPreview;
            deactivateImg.enabled = false;
            craftingImg.enabled = true;
            craftingNum.enabled = true;
            craftingNum.text = craft.craftingRecipe.resultingAmount.ToString();
        }

        preview = craft.craftingRecipe.craftPreview;
        
        if(!hasRecipeDataBeenGenerated) {
            GenerateRecipeData(craft);
            hasRecipeDataBeenGenerated = true;
        }
    }
    
    /// <summary>
    /// Function from the IPointerClickHandler to add a craft object to the inventory side
    /// </summary>
    /// <param name="eventData">Even handler for the point click</param>
    public void OnPointerClick(PointerEventData eventData) {
        if(craft != null && craft.craftingRecipe.resultingAmount > 0) {
            //playerInventory.CreatePreview(craft);
            craftInfo.SetActive(false);

            // Create a new item object
            Item item = new Item();
            // Copy the value in from the item representation of the craft object
            item.item = craft.craftingRecipe.itemRepresentation;
            item.amount = craft.craftingRecipe.itemRepresentation.itemAmount;

            // Add it to the player inventory
            playerInventory.playerInventory.Add(item);

            // Subtract the item cost of the crafting the object
            playerInventory.playerInventory.SubstractRecipeFromInventory(craft.craftingRecipe);

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
        if(craft == null) return;

        craftName.text = craft.craftingRecipe.recipeName;
        craftInfo.SetActive(true);
    }

    /// <summary>
    /// Function from the IPointerClickHandler to grab exit hovering from an object
    /// </summary>
    /// <param name="eventData">Even handler for the point click</param>
    public void OnPointerExit(PointerEventData eventData) {
        if(craft != null) {
            craftInfo.SetActive(false);
        }
    }

    /// <summary>
    /// Function to generate the data required for crafting this object. Loops through the length
    /// of the list of required items. Instantiates a gameobject and inserts the correct data required
    /// for crafting.
    /// </summary>
    /// <param name="Craft">the craft object to be checked</param>
    public void GenerateRecipeData(Craft craft) {
        for (int i = 0; i < craft.craftingRecipe.recipeItems.Length; i++) {
            GameObject hoverData = Instantiate(craftData, craftDisplay.transform, true);

            hoverData.transform.GetChild(0).GetComponent<Image>().sprite = craft.craftingRecipe.recipeItems[i].item.preview;
            hoverData.transform.GetChild(1).GetComponent<Text>().text = craft.craftingRecipe.recipeItems[i].amount.ToString();
            hoverData.transform.GetChild(2).GetComponent<Text>().text = "X";
            hoverData.transform.GetChild(3).GetComponent<Text>().text = craft.craftingRecipe.recipeItems[i].item.itemName;

            hoverData.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}