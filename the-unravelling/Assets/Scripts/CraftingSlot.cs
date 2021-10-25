using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour {
    ItemData craftItem;

    public Image craftingImg;
    public Text craftingNum;

    public void AddCraftingItem(ItemData newCraftItem)
    {
        craftItem = newCraftItem;
        craftingImg.sprite = craftItem.preview;
        craftingImg.enabled = true;
        craftingNum.enabled = true;
        craftingNum.text = craftItem.itemAmount.ToString();
    }

}
