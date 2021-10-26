using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A base class for units.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class BaseUnit : MonoBehaviour {
    // Maximum health of the unit. Configured in subclass or prefab.
    [SerializeField] protected int maxHealth;

    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;

    // Item that the unit drops when destroyed.
    [SerializeField] private ItemData drops;

    // Drop statistic of item `drops` from this unit.
    [SerializeField] private int minDropCount;
    [SerializeField] private int maxDropCount;

    // Current health of the unit.
    private int health;

    void Awake() {
        health = maxHealth;

        // Spawn health bar
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => HealthFraction;
    }

    /// <summary>
    /// Current health. Range: [0, maxHealth].
    /// </summary>
    public int Health => health;

    /// <summary>
    /// Current fraction of max health. Range: [0, 1].
    /// </summary>
    public float HealthFraction => (float)health / (float)maxHealth;

    /// <summary>
    /// Damage the unit. Destroy the unit if health goes to zero.
    /// </summary>
    /// <param name="damage">Points of damage</param>
    /// <returns>Was the building destroyed?</returns>
    public bool Damage(int damage) {
        health -= damage;

        // Was the building destroyed?
        if (health <= 0) {
            Destroy(this.gameObject);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Drop items based `drops` and on min and max drop count.
    /// </summary>
    private void Drop() {
        // Find the player and get the inventory
        var player = GameObject.FindWithTag("Player");
        var inventory = player.gameObject.GetComponent<PlayerBehaviour>().inventory;
        
        // Add the dropped item
        var count = Random.Range(minDropCount, maxDropCount);
        //inventory.AddItem(drops, count);
    }

    /// <summary>
    /// Called when health reaches zero, responsible for destroying the unit.
    /// </summary>
    private void OnDestroy() {
        Drop();
    }
}