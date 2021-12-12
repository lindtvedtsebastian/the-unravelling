using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class StoragePlayerItemSlot : Slot, IPointerClickHandler {

	protected InventoryWithStorage _storage;

    public void OnPointerClick(PointerEventData eventData) {
        
        _storage.TransferToStorage(item);
    }

    public void AddItemWithStorageReference(Item item) {

        _storage.TransferToStorage(item);
    }
}
