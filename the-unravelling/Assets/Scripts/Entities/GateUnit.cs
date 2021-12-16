using UnityEngine;

/// <summary>
/// A class for Gate units
/// </summary>
public class GateUnit : MonoBehaviour {
    private SpriteRenderer gateSprite;
    private BoxCollider2D _collider;

    void Start() {
        gateSprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        // Change collider radius
        _collider.size = new Vector2(1.2f, 1.2f);
    }

    void OnTriggerEnter2D(Collider2D col) {
        // Changing the alpha of the sprite when box collider is triggered
        gateSprite.color = new Color(1f, 1f, 1f, .5f);
    }

    void OnTriggerExit2D(Collider2D col) {
        // Changing the alpha back to normal when box collider trigger exit
        gateSprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
