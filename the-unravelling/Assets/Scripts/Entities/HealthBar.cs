using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class for the health bar
/// </summary>
public class HealthBar : MonoBehaviour {
    [SerializeField] private Image bar;
    [SerializeField] private Canvas canvas;

    public delegate float GetHealth();

    public GetHealth Health;

    private void Awake() {
        canvas.worldCamera = Camera.current;
    }

    private void Update() {
        if (Health == null) return;
        
        var health = Health();
        if (health >= 1.0f) {
            canvas.enabled = false;
        }
        else {
            canvas.enabled = true;
            bar.fillAmount = health;
        }
    }
}