using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldEntityHover : MonoBehaviour {
    [SerializeField]
    private Sprite _normal;

    [SerializeField]
    private Sprite _hovering;

    [SerializeField]
    private GameObject _hoverObject;
    private Canvas canvas;
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

        _hoverObject = Instantiate(_hoverObject, this.transform);

        canvas = _hoverObject.transform.GetChild(0).GetComponent<Canvas>();
        canvas.worldCamera = Camera.current;

        canvas.enabled = false;
    }

    /// <summary>
	/// Function to use the callback created for OnPointerEnter
	/// </summary>
    /// <param name="data">The Pointer event data</param>
    public void OnPointerEnterDelegate(PointerEventData data) {
        if(_sprite == null) return;

        if(gameObject.GetComponent<BaseUnit>().getObject().id == Constants.TREE ||
           gameObject.GetComponent<BaseUnit>().getObject().id == Constants.DRY_TREE) {
            canvas.transform.position = new Vector3(this.transform.position.x, 
                                                    this.transform.position.y - 1f, 
                                                    this.transform.position.z);
        }

        if(gameObject.GetComponent<BaseUnit>().getObject().id == Constants.WOOD_CHEST) {
            _sprite.sprite = _hovering;
        } else {
            _sprite.sprite = _sprite.sprite;
        }
        
        canvas.enabled = true;
    }

    /// <summary>
	/// Function to use the callback created for OnPointerExit
	/// </summary>
    /// <param name="data">The Pointer event data</param>
    public void OnPointerExitDelegate(PointerEventData data) {
        if(_sprite == null) return;

        if(gameObject.GetComponent<BaseUnit>().getObject().id == Constants.WOOD_CHEST) {
            _sprite.sprite = _normal;
        } else {
            _sprite.sprite = _sprite.sprite;
        }
        
        canvas.enabled = false;
    }
}