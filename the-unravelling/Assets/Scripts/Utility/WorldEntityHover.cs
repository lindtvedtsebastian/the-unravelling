using Unity.Assertions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WorldEntityHover : MonoBehaviour {
    [SerializeField]
    private Sprite _normal;

    [SerializeField]
    private Sprite _hovering;
    private SpriteRenderer _sprite;

    private EventTrigger _eventTrigger;

    void Start() {      
        _sprite = GetComponent<SpriteRenderer>(); 
        _eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener((data) => {OnPointerEnterDelegate((PointerEventData) data); });
        _eventTrigger.triggers.Add(enter);

        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((data) => {OnPointerExitDelegate((PointerEventData) data); });
        _eventTrigger.triggers.Add(exit);
    }

    public void OnPointerEnterDelegate(PointerEventData data) {
        Debug.Log("Hovering");
        if(_sprite == null) return;

        _sprite.sprite = _hovering;
    }

    public void OnPointerExitDelegate(PointerEventData data) {
        Debug.Log("Exit");
        if(_sprite == null) return;

        _sprite.sprite = _normal;
    }
}