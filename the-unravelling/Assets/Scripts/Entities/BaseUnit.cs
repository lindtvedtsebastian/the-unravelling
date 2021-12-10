using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A base class for units.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class BaseUnit : MonoBehaviour, IClickable {
    // Maximum health of the unit. Configured in subclass or prefab.
    [SerializeField] protected int maxHealth;

    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;

    // Item that the unit drops when destroyed.
    [SerializeField] private Item[] drops;

    // Current health of the unit.
    protected int health;

    protected virtual void Awake() {
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
    /// The action that will be triggered when this object is clicked
    /// </summary>
    /// <param name="damage">The amount of damage to inflict on the object</param>
    public void OnDamage(int damage) {
        health -= damage;

		if (health <= 0) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Generates "drop" gameobjects and scatters them around this entity
    /// </summary>
    private void Drop() {
        GameObject dropContainer = GameObject.FindWithTag("DropContainer");
        foreach (Item drop in drops) {
            Vector3 pos = gameObject.transform.position;

            for (int i = 0; i < drop.amount; i++) {
				Vector3 dropPos = new Vector3(Random.Range(pos.x - 1, pos.x + 1),
											  Random.Range(pos.y - 1, pos.y + 1),
											  pos.z);
				Instantiate(drop.item.manifestation,
							dropPos,
							Quaternion.identity,
							dropContainer.transform);
			}
        }
    }

    /// <summary>
    /// Called when health reaches zero, responsible for destroying the unit.
    /// </summary>
    protected virtual void OnDestroy() {
        Drop();
    }

}
