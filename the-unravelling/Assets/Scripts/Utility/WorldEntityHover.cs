using Unity.Assertions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WorldEntityHover : EventTrigger {
    [SerializeField]
    private Sprite closedChest;

    [SerializeField]
    private Sprite openChest;
    private SpriteRenderer _sprite;

    private EventTrigger _eventTrigger;

    void Start() {      
        _sprite = GetComponent<SpriteRenderer>();  

    }

    public override void OnPointerEnter(PointerEventData data) {
        
    }

/*     public void OnHoverEnter() {
        if(_sprite == null) return;
        _sprite.sprite = openChest;
    }

    public void OnHoverLeave() {
        if(_sprite == null) return;
        _sprite.sprite = closedChest;
    } */


}