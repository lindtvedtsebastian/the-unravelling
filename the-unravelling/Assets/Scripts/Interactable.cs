using System;
using UnityEngine;


public class Interactable : MonoBehaviour {
    
    public Item item;
    
    //private ItemSlot[] itemSlots;
    
    public GameObject player;

    public Inventory playerInventory;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = FindObjectOfType<Inventory>();
        //itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
    }
    
    private void OnTriggerEnter2D(Collider2D player)
    {
        Debug.Log(player.name + " picked up : " + item.item.itemName);
        playerInventory.Add(item);
        Destroy(gameObject);
    }
}

/*public virtual void Interact()
{
    //This method is meant to be overwritten
    //Debug.Log("Picked up : " + item.itemName);
    //FindObjectOfType<PlayerInventory>().Add(item);
    //Destroy(gameObject);
}*/
    
/*private void Update()
{
    float distance = Vector2.Distance(player.transform.position, transform.position);

    if (distance <= radius)
    {
        Interact();
    }
}*/