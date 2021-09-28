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
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory and Items/Inventory", order = 2)]
public class Inventory : ScriptableObject {
    // Stores the items in the inventory, where instance id is the key and the item data and item count is the value
    public InventoryKeyPair items = new InventoryKeyPair();

    // Does the inventory contain more than zero of this item
    public bool HasItem(in ItemData item) {
        return GetItemCount(item) > 0;
    }

    public int GetItemCount(in ItemData item) {
        // Try and get the item out of the inventory, but also check the count of the item in the inventory
        if (items.TryGetValue(item.GetInstanceID(), out var ii)) {
            return ii.count;
        }

        return 0;
    }

    /// <summary>
    /// Remove an item/items from the inventory.
    ///
    /// Items only count as removed, and this method returns true, if the inventory had the item in the first place.
    /// </summary>
    /// <param name="item">The item to remove</param>
    /// <param name="count">How many items to remove</param>
    /// <returns>Was the item removed?</returns>
    public bool RemoveItem(in ItemData item, int count = 1) {
        if (items.TryGetValue(item.GetInstanceID(), out var ii)) {
            ii.count -= count;
            if (ii.count < 0) ii.count = 0;
            return true;
        }

        return false;
    }

    // Inc the item count in the inventory
    public void AddItem(in ItemData item, int count = 1) {
        if (items.TryGetValue(item.GetInstanceID(), out var ii)) {
            ii.count += count;
        }
        else {
            ii = new InventoryItem(item);
            items.Add(item.GetInstanceID(), ii);
        }
    }

    public Dictionary<int, InventoryItem>.ValueCollection GetItems() {
        return items.Values;
    }
}