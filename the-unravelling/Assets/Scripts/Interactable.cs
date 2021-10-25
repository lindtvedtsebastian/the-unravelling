using UnityEngine;


public class Interactable : MonoBehaviour {
    private float radius = 1f;

    public ItemData item;
    
    public GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Picked up : " + item.itemName);
        FindObjectOfType<InventoryManager>().Add(item);
        Destroy(gameObject);
    }

    private void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= radius)
        {
            Interact();
        }
    }
}
