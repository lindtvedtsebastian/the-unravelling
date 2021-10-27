using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public delegate void OnClickCraftItem(in CraftingData item);

public class CraftingSlot : MonoBehaviour, IPointerClickHandler {
    //CraftingData craftItem;

    public Image craftingImg;
    public Text craftingNum;

    //private OnClickCraftItem callback;

    /*public void AddCraftingItem(CraftingData newCraftItem)
    {
        Debug.Log("name : " + newCraftItem.craftingName);
        craftItem = newCraftItem;
        craftingImg.sprite = craftItem.preview;
        craftingImg.enabled = true;
        craftingNum.enabled = true;
        craftingNum.text = craftItem.craftingAmount.ToString();
    }*/
    
    public void OnPointerClick(PointerEventData eventData) {
        /*if (!craftItem)
        {
            Debug.Log("Nothing to craft here yet!");
            return;
        }
        Debug.Log("Name of craft : " + craftItem.craftingName);
        FindObjectOfType<InventoryUIBehaviour>().CloseInventory(craftItem);*/
        //callback(craftItem);
    }
}
