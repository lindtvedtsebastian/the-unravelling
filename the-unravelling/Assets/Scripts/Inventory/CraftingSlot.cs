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
    public Craft craft;

    public Image craftingImg;
    public Image deactivateImg;
    public Text craftingNum;

    public GameObject craftInfo;
    public Text craftName;

    public DisplayRequirements craftDisplay;

    private OnClickCraft callback;

    public PlayerInventory playerInventory;
    
    private Mouse mouse;
    private Camera currentCamera;

    private Sprite preview;

    public GameObject previewCraft;

    void Start() {
        craftDisplay.GenerateRecipeData();
    }

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
        if(craft != null && craft.craftingRecipe.resultingAmount > 0) {
            playerInventory.CreatePreview(craft);
            Debug.Log("You can craft : " + craft.craftingRecipe.recipeName + " Now!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(craft == null) return;

        craftName.text = craft.craftingRecipe.recipeName;
        craftInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(craft != null)
            craftInfo.SetActive(false);
    }
}

// might need craft == null on condition if slot is empty
/* if (craft.craftingRecipe.resultingAmount < 1)
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
} */

//GameObject craftData = Instantiate(craftRequirement, ingredientPanel.transform, true);
//craftData.GetComponent<Text>().text = craft.craftingRecipe.recipeItems[i].amount.ToString();
//craftData.GetComponent<Text>().text = craft.craftingRecipe.recipeItems[i].item.itemName;

//Debug.Log("Name : " + craft.craftingRecipe.recipeItems[i].item.itemName);
//Debug.Log("Amount : " + craft.craftingRecipe.recipeItems[i].amount);

/* craftIngredient.sprite = craft.craftingRecipe.recipeItems[0].item.preview;
craftIngredientAmount.text = craft.craftingRecipe.recipeItems[0].amount.ToString();
craftIngredientName.text = craft.craftingRecipe.recipeItems[0].item.itemName; */

/* for (int i = 0; i < craft.craftingRecipe.recipeItems.Length; i++)
{
    DisplayData craftData = Instantiate(craftRequirement, ingredientPanel.transform, true);
    craftData.ingredientImg.sprite = craft.craftingRecipe.recipeItems[i].item.preview;
    craftData.ingredientAmount.text = craft.craftingRecipe.recipeItems[i].amount.ToString();
    craftData.ingredientName.text = craft.craftingRecipe.recipeItems[i].item.itemName;
} */
//craftInfo.AddComponent<DisplayData>();
//craftInfo.AddComponent(craftRequirement);

/* public Image craftIngredient;
public Text craftIngredientAmount;
public Text craftIngredientName; */

//public DisplayData craftRequirement;

//public Transform ingredientPanel;

//Destroy(craftDisplay);
//craftDisplay.ClearRecipeData();

//craftDisplay = Instantiate(craftDisplay);