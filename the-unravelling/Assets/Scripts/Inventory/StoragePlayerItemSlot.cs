using UnityEngine.EventSystems;
using UnityEngine;

public class StoragePlayerItemSlot : Slot, IPointerClickHandler {

	protected InventoryWithStorage _storage;

    [SerializeField]
    private StorageInventoryDisplay _storageDisplay;

    public void AddItemStorage(Item item, InventoryWithStorage storage) {
        _storage = storage;
        base.AddItem(item);

    }
    public void OnPointerClick(PointerEventData eventData) {
        _storage.TransferToStorage(1, item);

        _storageDisplay.RefreshStorageInventory(_storage);

        Debug.Log("Storage chest items list count : " + _storage.chestItems.Count);
    }
}
