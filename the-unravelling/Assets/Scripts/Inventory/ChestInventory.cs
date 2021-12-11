using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    public List<Item> chestItems;
    private List<Item> playerItems;

    public ChestInventory() {
        chestItems = new List<Item>();
        playerItems = _inventory.items;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
