using UnityEngine;

///<summary>
/// Class for displaying the chest storage display
///</summary>
public class StorageInventoryDisplay : MonoBehaviour {
    public GameObject chestInventoryCanvas;
    public Transform _playerPanel;
    public Transform _chestPanel;
    private StoragePlayerItemSlot[] itemSlots;
    private StorageItemSlot[] storageSlots;

    void Awake() {
        itemSlots = _playerPanel.GetComponentsInChildren<StoragePlayerItemSlot>();
		storageSlots = _chestPanel.GetComponentsInChildren<StorageItemSlot>();
    }

    ///<summary>
    /// Activate the chest storage inventory
    ///</summary>
    ///<param name="storage">The storage items to be added</param>
    ///<see cref="AddItems()"/>
    public void ActivateStorageInventory(InventoryWithStorage storage) {
        AddItems(storage);
        chestInventoryCanvas.SetActive(true);
    }

    ///<summary>
    /// Deactivate the chest storage inventory
    ///</summary>
    public void DeactivateStorageInventory() {
        chestInventoryCanvas.SetActive(false);
    }

    ///<summary>
    /// Refresh the chest storage inventory
    ///</summary>
    ///<param name="storage">The storage items</param>
    ///<see cref="DeactivateStorageInventory()"/>
    ///<see cref="ActivateStorageInventory()"/>
    public void RefreshStorageInventory(InventoryWithStorage storage) {
        DeactivateStorageInventory();
        ActivateStorageInventory(storage);
    }

    ///<summary>
    /// Add items to the chest storage inventory 
    ///</summary>
    ///<param name="storage">The storage items</param>
    ///<see cref="removeEmpty()"/>
    ///<see cref="ClearData()"/>
    ///<see cref="AddItemStorage()"/>
    private void AddItems(InventoryWithStorage storage) {
        storage.player.removeEmpty();
        
        for(int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].ClearData();
            if(i < storage.player.items.Count) {
                itemSlots[i].AddItemStorage(storage.player.items[i], storage);
            }
        }

        storage.storage.removeEmpty();

        for(int i = 0; i < storageSlots.Length; i++) {
            storageSlots[i].ClearData();
            if(i < storage.storage.items.Count) {
                storageSlots[i].AddItemStorage(storage.storage.items[i], storage);
            }
        }
    }

}
