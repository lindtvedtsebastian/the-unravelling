using Unity.Assertions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour {
    [SerializeField]
    private Sprite closedChest;

    [SerializeField]
    private Sprite openChest;

    private BoxCollider2D _collider;
    private SpriteRenderer _sprite;

    private float colliderRadius = 2f;

    public bool canOpenChest;
    
    // Global objects
    private Mouse mouse;
    private Camera currentCamera;

    void Start() {
        //_collider = GetComponent<BoxCollider2D>();      
        _sprite = GetComponent<SpriteRenderer>();  

        //_collider.offset = new Vector2(0, -1f);

        canOpenChest = false;

        // Grab global objects
        mouse = Mouse.current;
        currentCamera = Camera.main;

        Assert.IsNotNull(mouse, "No mouse found");
        Assert.IsNotNull(currentCamera, "No main camera set");  
    }

    public void OnHoverEnter() {
        Debug.Log("Hovering this object : " + gameObject.name);
        _sprite.sprite = openChest;
        canOpenChest = true;
    }

    public void OnHoverLeave() {
        Debug.Log("Exit hovering : " + gameObject.name);
        _sprite.sprite = closedChest;
        canOpenChest = false;
    }
}