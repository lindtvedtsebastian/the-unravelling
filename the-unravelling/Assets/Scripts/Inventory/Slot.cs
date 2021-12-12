using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {	
    
    Item item;
	public Image itemImg;
	public Text itemNum;

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
}
