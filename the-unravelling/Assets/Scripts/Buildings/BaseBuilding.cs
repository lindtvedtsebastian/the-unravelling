using System;
using UnityEngine;

/// <summary>
/// A base class for buildings.
/// </summary>
public class BaseBuilding : MonoBehaviour {
    /// <summary>
    /// Maximum health of the building. Configured in subclass or prefab.
    /// </summary>
    [SerializeField] protected int maxHealth;

    [SerializeField] private GameObject healthBar;

    // Current health
    private int health;

    void Awake() {
        health = maxHealth;

        // Spawn health bar
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => HealthPercentage;
    }

    /// <summary>
    /// Getter for health.
    /// </summary>
    public int Health => health;

    /// <summary>
    /// Current percentage of max health.
    /// </summary>
    public float HealthPercentage => (float)health / (float)maxHealth;

    /// <summary>
    /// Damage the building. Destroy the building if health goes to zero.
    /// </summary>
    /// <param name="damage">Points of damage</param>
    /// <returns>Was the building destroyed</returns>
    public bool Damage(int damage) {
        health = health - damage;

        // Was the building destroyed?
        if (health <= 0) {
            OnShouldDestroy();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Called when health reaches zero, responsible for destroying the building.
    ///
    /// NOTE: Overriders should call the provided method last, as it destroys the building.
    /// </summary>
    protected virtual void OnShouldDestroy() {
        Destroy(this.gameObject);
    }
}