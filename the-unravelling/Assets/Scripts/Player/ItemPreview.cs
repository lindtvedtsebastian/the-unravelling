using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnClick(in ItemData item);

/// <summary>
/// UI Element that is instantiated by the inventory UI, representing a single type of item in the inventory.
/// </summary>
public class ItemPreview : MonoBehaviour, IPointerClickHandler {
    // Actual inventory item
    private InventoryItem item;

    // Inventory UI callback, called when the element is clicked
    private OnClick callback;

    /// <summary>
    /// Add the item data to the preview.
    /// </summary>
    /// <param name="itemData">Item data</param>
    /// <param name="onClick">OnClick callback, called when the element is clicked</param>
    public void AddItemData(in InventoryItem itemData, OnClick onClick) {
        item = itemData;
        callback = onClick;

        // Get the child elements
        var name = transform.GetChild(0).gameObject.GetComponent<Text>();
        var count = transform.GetChild(1).gameObject.GetComponent<Text>();

        // Set the item information
        name.text = itemData.data.itemName;
        count.text = itemData.count.ToString();

        // Set preview
        var image = GetComponent<Image>();
        image.sprite = itemData.data.preview;
    }

    /// <summary>
    /// Called when the element is clicked by a pointer.
    /// </summary>
    /// <param name="eventData">Pointer event data</param>
    public void OnPointerClick(PointerEventData eventData) {
        callback(item.data);
    }
}