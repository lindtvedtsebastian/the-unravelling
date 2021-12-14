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
        if(item == null) return;
        _storage.TransferToStorage(item);
        _storageDisplay.RefreshStorageInventory(_storage);
    }
}
