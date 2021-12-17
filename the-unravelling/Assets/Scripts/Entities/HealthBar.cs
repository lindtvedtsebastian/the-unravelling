using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behaviour for the health bar.
/// </summary>
public class HealthBar : MonoBehaviour {
    [SerializeField] private Image bar;
    [SerializeField] private Canvas canvas;

    public delegate float GetHealth();

    // Hook for the health bar to dynamically update it's state.
    // Should be in interval [0.0, 1.0].
    public GetHealth Health;

    private void Awake() {
        canvas.worldCamera = Camera.current;
    }

    private void Update() {
        if (Health == null) return;
        
        // Get the current health from the hook
        var health = Health();
        
        // Show or hide the health bar based on whether the entity has taken some damage.
        if (health >= 1.0f) {
            canvas.enabled = false;
        }
        else {
            canvas.enabled = true;
            bar.fillAmount = health;
        }
    }
}