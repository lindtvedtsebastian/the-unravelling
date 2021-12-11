using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour {
    [SerializeField]
    private Sprite closedChest;

    [SerializeField]
    private Sprite openChest;

    private BoxCollider2D _collider;
    private SpriteRenderer _sprite;

    private float colliderRadius = 2f;

    public bool canOpenChest;

    void Start() {
        //_collider = GetComponent<BoxCollider2D>();      
        _sprite = GetComponent<SpriteRenderer>();  

        //_collider.offset = new Vector2(0, -1f);

        canOpenChest = false;
    }

/*     /// <summary>
    /// Function to catch the BoxCollider2D trigger enter 
    /// </summary>
    /// <param name="col">The collider</param>
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Close enough to trigger");
        _sprite.sprite = openChest;
        canOpenChest = true;
    }

    /// <summary>
    /// Function to catch the BoxCollider2D trigger exit
    /// </summary>
    /// <param name="col">The collider</param>
    void OnTriggerExit2D(Collider2D col) {
        Debug.Log("Exit chest trigger");
        _sprite.sprite = closedChest;
        canOpenChest = false;
    } */

   /*  public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering over : " + gameObject.name);
        _sprite.sprite = openChest;
        canOpenChest = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Stop hovering over : " + gameObject.name);
        _sprite.sprite = closedChest;
        canOpenChest = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on : " + gameObject.name);
    } */
}