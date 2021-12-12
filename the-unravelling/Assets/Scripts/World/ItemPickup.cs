using System;
using UnityEngine;

/// <summary>
/// A class representing the interaction of an item
/// </summary>
public class ItemPickup : MonoBehaviour {
    
    [SerializeField]
    private Item item;
    
    private GameObject player;
    
    private Inventory playerInventory;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }
    
    /// <summary>
    /// Function to handle the on trigger event
    /// </summary>
    /// <param name="other">The object that collides with this gameobject</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInventory.Add(item);
        Destroy(gameObject);
    }
}