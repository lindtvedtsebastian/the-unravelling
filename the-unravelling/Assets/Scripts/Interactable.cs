using System;
using UnityEngine;


public class Interactable : MonoBehaviour {
    
    public ItemData item;
    
    public GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D player)
    {
        Debug.Log(player.name + " picked up : " + item.itemName);
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