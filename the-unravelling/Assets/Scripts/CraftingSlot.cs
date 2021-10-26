using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnClickCraftItem(in ItemData item);

public class CraftingSlot : MonoBehaviour, IPointerClickHandler {
    ItemData craftItem;

    public Image craftingImg;
    public Text craftingNum;

    private OnClickCraftItem callback;

    public void AddCraftingItem(ItemData newCraftItem)
    {
        craftItem = newCraftItem;
        craftingImg.sprite = craftItem.preview;
        craftingImg.enabled = true;
        craftingNum.enabled = true;
        craftingNum.text = craftItem.itemAmount.ToString();
    }
    
    public void OnPointerClick(PointerEventData eventData) {
        if (!craftItem)
        {
            Debug.Log("Nothing to craft here yet!");
            return;
        }
        Debug.Log("Name of craft : " + craftItem.itemName);
        FindObjectOfType<InventoryUIBehaviour>().CloseInventory(craftItem);
        //callback(craftItem);
    }
}
