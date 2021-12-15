using UnityEngine.EventSystems;
using UnityEngine;

///<summary>
/// Child class of Slot that represents a storage player item slot
///</summary>
public class StoragePlayerItemSlot : Slot, IPointerClickHandler {

	protected InventoryWithStorage _storage;

    [SerializeField]
    private StorageInventoryDisplay _storageDisplay;

    /// <summary>
    /// Add items to the chest storage player inventory 
    /// </summary>
    /// <param name="item">item to be added</param>
    /// <param name="storage">The storage that the item is added to</param>
    /// <see cref="AddItem()"/>
    public void AddItemStorage(Item item, InventoryWithStorage storage) {
        _storage = storage;
        base.AddItem(item);
    }
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Slot name : " + gameObject.name);
        if(item == null) return;
        _storage.TransferToStorage(item);
        _storageDisplay.RefreshStorageInventory(_storage);
    }
}
