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
        _storage.TransferToStorage(item);

        _storageDisplay.RefreshStorageInventory(_storage);

        for(int i = 0; i < _storage.storage.items.Count; i++) {
            Debug.Log("Content player : " + _storage.player.items[i].amount);
            Debug.Log("Content storage : " + _storage.storage.items[i].amount);
        }

        //Debug.Log("Storage chest items list count : " + _storage.storage.items.Count);
    }
}
