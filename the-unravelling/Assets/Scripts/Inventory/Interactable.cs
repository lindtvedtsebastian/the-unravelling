using Unity.Assertions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour {
    [SerializeField]
    private Sprite closedChest;

    [SerializeField]
    private Sprite openChest;

    [SerializeField]
    private ChestInventory _chestInventory;

    private SpriteRenderer _sprite;

    private float colliderRadius = 2f;

    void Start() {      
        _sprite = GetComponent<SpriteRenderer>();  
        _chestInventory = GetComponent<ChestInventory>();
    }

    public void OnHoverEnter() {
        Debug.Log("Hovering this object : " + gameObject.name);
        _sprite.sprite = openChest;
    }

    public void OnHoverLeave() {
        Debug.Log("Exit hovering : " + gameObject.name);
        _sprite.sprite = closedChest;
    }

    public void OpenChestInventory() {
        Debug.Log("Opening chest inventory");
        _chestInventory.ActivateChestInventory();
    }

    public void CloseChestInventory() {
        _chestInventory.DeactivateChestInventory();
    }
}