using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class representing a item slot in the inventory
/// </summary>
public class ItemSlot : Slot, IPointerClickHandler {
	public PlayerInventoryDisplay playerInventory;

    public void OnPointerClick(PointerEventData eventData) {
		if(item != null) {
			playerInventory.CreatePreview(item);
		} 	
    }
}
