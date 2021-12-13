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
        if(_sprite == null) return;
        _sprite.sprite = openChest;
    }

    public void OnHoverLeave() {
        if(_sprite == null) return;
        _sprite.sprite = closedChest;
    }
}