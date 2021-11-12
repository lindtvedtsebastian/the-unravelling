using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class representing a item slot in the inventory
/// </summary>
public class ItemSlot : MonoBehaviour, IPointerClickHandler {
	Item item;

	public Image itemImg;
	public Text itemNum;

	public PlayerInventory playerInventory;
	
	/// <summary>
	/// Function to add item to the item slots in the inventory
	/// </summary>
	public void AddItem(Item newItem) {
		item = newItem;
		itemImg.sprite = item.item.preview;
		itemImg.enabled = true;
		itemNum.enabled = true;
		itemNum.text = item.amount.ToString();
	}
	
	/// <summary>
	/// Function to clear the item data in the inventory
	/// </summary>
	public void ClearData() {
		item = null;
		
        itemImg.enabled = false;
        itemNum.enabled = false;

        itemImg.sprite = null;
        itemNum.text = null;
    }

    public void OnPointerClick(PointerEventData eventData) {
		if(item != null) {
			Debug.Log("Clicked on item : " + item.item.itemName + " with amount : " + item.amount);
			playerInventory.isItemEmpty = false;
			playerInventory.CreatePreview(item);
		} 	
    }
}
