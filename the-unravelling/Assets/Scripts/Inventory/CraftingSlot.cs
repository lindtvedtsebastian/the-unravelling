using Unity.Assertions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public delegate void OnClickCraft(in Craft craftObject);

public class CraftingSlot : MonoBehaviour, IPointerClickHandler {
    Craft craft;

    public Image craftingImg;
    public Image deactivateImg;
    public Text craftingNum;

    private OnClickCraft callback;

    public VisualizeInventory playerInventory;
    
    private Mouse mouse;
    private Camera currentCamera;

    private Sprite preview;

    public GameObject previewCraft;

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
            // Debug.Log("name : " + newCraft.craftingRecipe.recipeName);
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
            //CreatePreview();
            //playerInventory.GrabCraftingFromInventory(craft);
            Debug.Log("You can craft : " + craft.craftingRecipe.recipeName + " Now!");
        }
    }
}
