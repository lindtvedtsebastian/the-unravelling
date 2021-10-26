using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInventory: MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public List<ItemData> items = new List<ItemData>();
    //public List<CraftingData> craftItems = new List<CraftingData>();

    //public CraftingData turret;

    public void Add(ItemData item)
    {
        ItemData copyItem = Instantiate(item);

        for (int i = 0; i < items.Count; i++)
        {
            if (item.itemName == items[i].itemName)
            {
                items[i].itemAmount++;
                
                //CheckItemsForCrafting(items, turret);
                
                if(onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                
                return;
            }
        }
        
        items.Add(copyItem);
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        
        //CheckItemsForCrafting(items, turret);
    }

    /*public void CheckItemsForCrafting(List<ItemData> items, CraftingData craftingItem)
    {
        //craftItems = new List<CraftingData>();
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
            //Debug.Log("We can build a : " + craftingItem.craftingName);
            craftItems.Add(craftingItem);
        }
    }*/
}
