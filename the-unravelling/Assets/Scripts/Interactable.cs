using System;
using UnityEngine;


public class Interactable : MonoBehaviour {
    
    public Item item;
    public GameObject player;
    public Inventory playerInventory;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
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
//Debug.Log(player.name + " picked up : " + item.item.itemName);
//itemSlots = itemPanel.GetComponentsInChildren<ItemSlot>();
//private ItemSlot[] itemSlots;