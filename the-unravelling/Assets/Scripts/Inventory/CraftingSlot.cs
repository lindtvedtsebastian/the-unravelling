using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnClickCraft(in Craft craftObject);

public class CraftingSlot : MonoBehaviour, IPointerClickHandler {
    Craft craft;

    public Image craftingImg;
    public Text craftingNum;

    private OnClickCraft callback;

    public void AddCraftingItem(Craft newCraft)
    {
        //Debug.Log("name : " + newCraft.craftingRecipe.recipeName);
        craft = newCraft;
        craftingImg.sprite = craft.craftingRecipe.craftPreview;
        craftingImg.enabled = true;
        craftingNum.enabled = true;
        craftingNum.text = craft.craftingRecipe.resultingAmount.ToString();
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        // might need craft == 0 on condition if slot is empty
        if (craft.craftingRecipe.resultingAmount < 1)
        {
            Debug.Log("Can't craft : " + craft.craftingRecipe.recipeName + " yet!");
            return;
        }
        Debug.Log("Name of craft : " + craft.craftingRecipe.recipeName);
        //FindObjectOfType<InventoryUIBehaviour>().CloseInventory(craftItem);
        callback(craft);
    }
}
