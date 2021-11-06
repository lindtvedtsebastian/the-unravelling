using Unity.Assertions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public delegate void OnClickCraft(in Craft craftObject);

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
    public Image craftIngredient;
    public Text craftIngredientAmount;
    public Text craftIngredientName;

    private OnClickCraft callback;

    public PlayerInventory playerInventory;
    
    private Mouse mouse;
    private Camera currentCamera;

    private Sprite preview;

    public GameObject previewCraft;

    /// <summary>
    /// Function to add a craft object to the inventory
    /// </summary>
    public void AddCraftingItem(Craft newCraft)
    {
        craft = newCraft;
        
        if (craft.craftingRecipe.resultingAmount < 1)
        {
            deactivateImg.sprite = craft.craftingRecipe.craftPreview;
            deactivateImg.enabled = true;
            craftingImg.enabled = false;
            craftingNum.enabled = false;
        }
        else
        {
            craftingImg.sprite = craft.craftingRecipe.craftPreview;
            deactivateImg.enabled = false;
            craftingImg.enabled = true;
            craftingNum.enabled = true;
            craftingNum.text = craft.craftingRecipe.resultingAmount.ToString();
        }
        
        mouse = Mouse.current;
        currentCamera = Camera.main;

        preview = craft.craftingRecipe.craftPreview;
        
        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");
    }

    
    /// <summary>
    /// Function from the IPointerClickHandler to grab a pointer click
    /// </summary>
    /// <param name="eventData">Even handler for the point click</param>
    public void OnPointerClick(PointerEventData eventData) {
        // might need craft == null on condition if slot is empty
        if (craft.craftingRecipe.resultingAmount < 1)
        {
            Debug.Log("Can't craft : " + craft.craftingRecipe.recipeName + " yet!");
        } else if (craft == null)
        {
            Debug.Log("Nothing in this slot");
        }
        else
        {
            playerInventory.CreatePreview(craft);

            Debug.Log("You can craft : " + craft.craftingRecipe.recipeName + " Now!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        craftName.text = craft.craftingRecipe.recipeName;
        craftIngredient.sprite = craft.craftingRecipe.recipeItems[0].item.preview;
        craftIngredientAmount.text = craft.craftingRecipe.recipeItems[0].amount.ToString();
        craftIngredientName.text = craft.craftingRecipe.recipeItems[0].item.itemName;
        craftInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        craftInfo.SetActive(false);
    }
}
