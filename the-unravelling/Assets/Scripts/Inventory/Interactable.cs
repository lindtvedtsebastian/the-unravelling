using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField]
    private Sprite closedChest;

    [SerializeField]
    private Sprite openChest;

    private CircleCollider2D _collider;
    private SpriteRenderer _sprite;

    private float colliderRadius = 2f;

    public bool canOpenChest;

    // Start is called before the first frame update
    void Start() {
        _collider = GetComponent<CircleCollider2D>();      
        _sprite = GetComponent<SpriteRenderer>();  

        _collider.radius = colliderRadius;

        canOpenChest = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Close enough to trigger");
        _sprite.sprite = openChest;
        canOpenChest = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("Exit chest trigger");
        _sprite.sprite = closedChest;
        canOpenChest = false;
    }

}
