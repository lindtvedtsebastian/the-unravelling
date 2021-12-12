using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageItemSlot : Slot, IPointerClickHandler
{
	protected InventoryWithStorage _storage;

    [SerializeField]
    private StorageInventoryDisplay _storageDisplay;

    public void AddItemStorage(Item item, InventoryWithStorage storage) {
        _storage = storage;
        base.AddItem(item);

    }
    public void OnPointerClick(PointerEventData eventData) {
        _storage.TransferFromStorage(item);
        _storageDisplay.RefreshStorageInventory(_storage);
    }
}
