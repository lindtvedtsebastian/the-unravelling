using System;
using System.Collections.Generic;
using UnityEngine;

// A stupid hack to make unity serialize dictionaries
public abstract class
    SerializedDictionary<Key, Value> : Dictionary<Key, Value>, ISerializationCallbackReceiver {
    // The dictionary is serialized as two arrays
    [SerializeField, HideInInspector] private List<Key> _keys = new List<Key>();
    [SerializeField, HideInInspector] private List<Value> _values = new List<Value>();

    void ISerializationCallbackReceiver.OnAfterDeserialize() {
        Clear();
        for (int i = 0; i < _keys.Count && i < _values.Count; i++) {
            this[_keys[i]] = _values[i];
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize() {
        _keys.Clear();
        _values.Clear();

        foreach (var pair in this) {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
    }
}

[Serializable]
public struct InventoryItem {
    public ItemData data;
    public int count;

    public InventoryItem(ItemData d, int c = 1) {
        data = d;
        count = c;
    }
}

// Specialize the serializable dictionary to one for inventory items.
// Key = instance id, value = inventory item.
[Serializable]
public class InventoryKeyPair : SerializedDictionary<int, InventoryItem> {
}

/// <summary>
/// Inventory represents a player inventory.
/// Being a ScriptableObject makes us able to predefine specific inventory presets for tutorials and player spawn etc.
///
/// The idea is that all the items are pre-generated as assets, and the rest of the game will have refs to items. The inventory only keeps track of what items and how many of them, the owner entity has.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Items/Inventory", order = 2)]
public class InventoryClass : ScriptableObject {
    // Stores the items in the inventory, where instance id is the key and the item data and item count is the value
    public InventoryKeyPair items = new InventoryKeyPair();

    /// <summary>
    /// Does the inventory contain more than zero of this item.
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns>Inventory has item?</returns>
    public bool HasItem(in ItemData item) {
        return GetItemCount(item) > 0;
    }

    /// <summary>
    /// Get the item count for the given item.
    /// </summary>
    /// <param name="item">Item</param>
    /// <returns>Item count</returns>
    public int GetItemCount(in ItemData item) {
        // Try and get the item out of the inventory, but also check the count of the item in the inventory
        if (items.TryGetValue(item.GetInstanceID(), out var ii)) {
            return ii.count;
        }

        return 0;
    }

    /// <summary>
    /// Remove an item/items from the inventory.
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="count">How many items to remove</param>
    /// <returns>Was the item removed?</returns>
    public bool RemoveItem(in ItemData item, int count = 1) {
        if (items.TryGetValue(item.GetInstanceID(), out var ii)) {
            if (ii.count < count) return false;
            
            ii.count -= count;
            items[item.GetInstanceID()] = ii;
            return true;
        }

        return false;
    }

    // Inc the item count in the inventory
    /// <summary>
    /// Add one or more items to the inventory.
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <param name="count">How many to add</param>
    public void AddItem(in ItemData item, int count = 1) {
        if (items.TryGetValue(item.GetInstanceID(), out var ii)) {
            ii.count += count;
            items[item.GetInstanceID()] = ii;
        }
        else {
            ii = new InventoryItem(item, count);
            items.Add(item.GetInstanceID(), ii);
        }
    }

    /// <summary>
    /// Get a list of all the unique items in the inventory and their count.
    /// </summary>
    /// <returns>List of unique items in the inventory</returns>
    public Dictionary<int, InventoryItem>.ValueCollection GetItems() {
        return items.Values;
    }
}
