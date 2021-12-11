using Unity.Assertions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour {
    [SerializeField]
    private Sprite closedChest;

    [SerializeField]
    private Sprite openChest;
    private SpriteRenderer _sprite;

    void Start() {      
        _sprite = GetComponent<SpriteRenderer>();  
    }

    public void OnHoverEnter() {
        Debug.Log("Hovering this object : " + gameObject.name);
        _sprite.sprite = openChest;
    }

    public void OnHoverLeave() {
        Debug.Log("Exit hovering : " + gameObject.name);
        _sprite.sprite = closedChest;
    }
}