using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Base class for inventory slots
///</summary>
public class Slot : MonoBehaviour {	
    
    protected Item item;

    [SerializeField]
	protected Image itemImg;

    [SerializeField]
	protected Text itemNum;

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
