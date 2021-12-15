using System;
using System.Collections;
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

    [SerializeField]
    private WorldEntity _self;

    [SerializeField]
    private Sprite[] _spriteArray;

    private int currentSprite = 0;

    // Current health of the unit.
    protected int health;

    private World _world;

    private bool recentlyDamaged = false;

    void Awake() {
	    _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
        health = maxHealth;

        // Spawn health bar
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => HealthFraction;
    }

    private void Update() {
	    if (recentlyDamaged) {
		    recentlyDamaged = false;
		    StartCoroutine(regenerateAfterDuration());
	    }
    }

    /// <summary>
    /// Current health. Range: [0, maxHealth].
    /// </summary>
    public int Health => health;

    /// <summary>
    /// Current fraction of max health. Range: [0, 1].
    /// </summary>
    public float HealthFraction => (float)health / (float)maxHealth;

    public void NextSprite(SpriteRenderer sprite) { 
        currentSprite++;

        if (currentSprite >= _spriteArray.Length) currentSprite = 0;
        
        var _sprite = sprite;
        sprite.sprite = _spriteArray[currentSprite];

        
    }

    /// <summary>
    /// The action that will be triggered when this object is clicked
    /// </summary>
    /// <param name="damage">The amount of damage to inflict on the object</param>
    public void OnDamage(int damage, bool damageFromMachine) {
        health -= damage;
        if (!damageFromMachine) {
			recentlyDamaged = true;
        }

		if (health <= 0) {
			Drop();
			int y = _world.size - Mathf.FloorToInt(gameObject.transform.position.y);
            int x = Mathf.FloorToInt(gameObject.transform.position.x);
            _world.entities[y][x] = 0;
            gameObject.SetActive(false);
            Destroy(gameObject);

		}
    }

    IEnumerator regenerateAfterDuration(float time = 5f) {
	    yield return new WaitForSeconds(time);
		health = maxHealth;
    }

    /// <summary>
    /// Generates "drop" gameobjects and scatters them around this entity
    /// </summary>
    private void Drop() {
        GameObject dropContainer = GameObject.FindWithTag("DropContainer");
        foreach (var drop in _self.drops) {
            Vector3 pos = gameObject.transform.position;

            for (int i = 0; i < drop.amount; i++) {
				Vector3 dropPos = new Vector3(Random.Range(pos.x - 1, pos.x + 1),
											  Random.Range(pos.y - 1, pos.y + 1),
											  pos.z);
				Instantiate(drop.dropObject,
							dropPos,
							Quaternion.identity,
							dropContainer.transform);
			}
        }
    }

    public WorldEntity getObject() {
        return _self;
    }

}
