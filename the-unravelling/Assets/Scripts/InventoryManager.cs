using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryManager: MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public List<ItemData> items = new List<ItemData>();
    public List<ItemData> craftItems = new List<ItemData>();

    public ItemData turret;

    public void Add(ItemData item)
    {
        ItemData copyItem = Instantiate(item);

        for (int i = 0; i < items.Count; i++)
        {
            if (item.itemName == items[i].itemName)
            {
                items[i].itemAmount++;
                
                CheckItemsForCrafting(items, turret);
                
                if(onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                
                //Debug.Log("Item name : " +  items[i].itemName + " Item count : " + items[i].itemAmount);
                return;
            }
        }
        
        items.Add(copyItem);
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        
        CheckItemsForCrafting(items, turret);
    }

    public void CheckItemsForCrafting(List<ItemData> items, ItemData item)
    {
        craftItems = new List<ItemData>();
        bool enoughStone = false;
        bool enoughWood = false;
        
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == "WoodItem" && items[i].itemAmount >= 2)
            {
                enoughWood = true;
            }

            if (items[i].itemName == "StoneItem" && items[i].itemAmount >= 1)
            {
                enoughStone = true;
            }
        }

        if (enoughWood && enoughStone)
        {
            Debug.Log("We can build a : " + item.itemName);
            craftItems.Add(item);
        }
    }

    public void DisplayAmount()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log("The items in the inventory are : " + items[i].itemName);
        }
    }
}
