using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryManager: MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public List<ItemData> items = new List<ItemData>();

    public void Add(ItemData item)
    {
        ItemData copyItem = Instantiate(item);

        for (int i = 0; i < items.Count; i++)
        {
            if (item.itemName == items[i].itemName)
            {
                items[i].itemAmount++;
                
                if(onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                
                Debug.Log("Item name : " +  items[i].itemName + " Item count : " + items[i].itemAmount);
                return;
            }
        }
        
        items.Add(copyItem);
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void DisplayAmount()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("The items in the inventory are : " + items[i].itemName);
        }
    }
}
