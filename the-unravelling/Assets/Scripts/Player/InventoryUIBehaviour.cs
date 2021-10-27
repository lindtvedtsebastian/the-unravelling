using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

//public delegate void OnClickInventory(in CraftingData item);

/// <summary>
/// The inventory UI Behaviour drives the UI for the inventory, by handling opening and closing the UI, as well as keeping the list of items up to data.
/// </summary>
public class InventoryUIBehaviour : MonoBehaviour {
    // Prefab for the item to be instantiated
    public ItemPreview itemPrefab;

    // The panel that is part of the UI
    public GameObject panel;

    // Player callback, called when the inventory ui closes
    //private OnClickInventory callback;
    
    /*public void OnActivate(in Inventory inventory, OnClickInventory click) {
        callback = click;
        
        Debug.Log("This happens");

        // Activate the UI
        gameObject.SetActive(true);

        // Create previews for all the items in the inventory
        //foreach (var item in inventory.GetItems()) {
            //var cell = Instantiate(itemPrefab, panel.transform, true);
            //cell.AddItemData(item, CloseInventory);
        //}
    }*/
    
    /*public void CloseInventory(in CraftingData item) {
        callback(item);
        gameObject.SetActive(false);
        
        // Destroy all previous elements
        //for (var i = 0; i < panel.transform.childCount; i++) {
            //Destroy(panel.transform.GetChild(i).gameObject);
        //}
    }

    /// <summary>
    /// Called by the Cancel action when the inventory UI should be closed.
    /// </summary>
    /// <param name="ctx">Callback context</param>
    public void OnClose(InputAction.CallbackContext ctx) {
        CloseInventory(null);
    }*/
}