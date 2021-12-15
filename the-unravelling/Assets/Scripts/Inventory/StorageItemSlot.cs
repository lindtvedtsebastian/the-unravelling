using UnityEngine;
using UnityEngine.EventSystems;

///<summary>
/// Child class of Slot that represents a storage item slot
///</summary>
public class StorageItemSlot : Slot, IPointerClickHandler {
	protected InventoryWithStorage _storage;

    [SerializeField]
    private StorageInventoryDisplay _storageDisplay;

    /// <summary>
    /// Add items to the chest storage inventory 
    /// </summary>
    /// <param name="item">item to be added</param>
    /// <param name="storage">The storage that the item is added to</param>
    /// <see cref="AddItem()"/>
    public void AddItemStorage(Item item, InventoryWithStorage storage) {
        _storage = storage;
        base.AddItem(item);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(item == null) return;

        if (gameObject.name.Contains("StoragePlayerItemSlot")) _storage.TransferToStorage(item);
        else if(gameObject.name.Contains("StorageItemSlot")) _storage.TransferFromStorage(item);
        
        _storageDisplay.RefreshStorageInventory(_storage);
    }
}
