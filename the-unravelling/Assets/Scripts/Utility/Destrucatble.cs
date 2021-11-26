
using System;
using UnityEngine;


/// <summary>
/// A base class for units.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Destrucatble : MonoBehaviour, IClickable{
    
    [SerializeField] protected int maxHealth;

    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;
     
    // Current health of the unit.
    protected int health;
   
    //Can regenerate health?
    private bool isHealthRegenerable; 
    
    
    
    
    void Awake() {
        health = maxHealth;

        // Spawn health bar
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => HealthFraction;
    }
    
    
    /// <summary>
    /// Current fraction of max health. Range: [0, 1].
    /// </summary>
    public float HealthFraction => (float)health / (float)maxHealth;
    
    
    public void OnDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy();
        }
    }

    private void OnDestroy() {
        
    }
}
