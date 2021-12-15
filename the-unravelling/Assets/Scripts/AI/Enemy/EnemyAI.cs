using System.Collections;
using UnityEngine;

public class EnemyAI : StateManager {

    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;
    [SerializeField] private int health;
    
    // Attack damage
    [SerializeField] private int attackDamage = 10;

    // Attack range in unity units
    [SerializeField] private float attackRange = 2.0f;
    
    // Attack cooldown in seconds
    [SerializeField] private float attackCooldown = 1.0f;

    private WaveManager waveManager;
    public EnemyWalk enemyWalk;
    public EnemyIdle enemyIdle;

    // Start is called before the first frame update
    void Start() {
        health = 100;
        // Spawn health bar
        var bar = Instantiate(healthBar, this.transform);
        var data = bar.GetComponent<HealthBar>();
        data.Health += () => health / 100f;
		
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();

        enemyWalk = gameObject.AddComponent(typeof(EnemyWalk)) as EnemyWalk;
        enemyIdle = gameObject.AddComponent(typeof(EnemyIdle)) as EnemyIdle;

        setState(enemyWalk);
        currentState.EnterState(this);

        StartCoroutine(PerformAttack());
    }

	void Update() {
        currentState.DoState();
    }
	
	/// <summary>
    /// The action that will be triggered when this enemy is damaged 
    /// </summary>
    /// <param name="damage">The amount of damage to inflict on the enemy</param>
    public void OnDamage(int damage) {
        health -= damage;

		if (health <= 0) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Perform attack on player if the player is in range.
    /// Attack stats are taken from `attackDamage`, `attackRange` and `attackCooldown`.
    /// </summary>
    IEnumerator PerformAttack() {
        var player = FindObjectOfType<PlayerBehaviour>();
        
        while (true) {
            var distance = Vector2.Distance(player.transform.position, this.transform.position);
            
            // If the player is in range, do damage
            if (distance <= attackRange) {
                if (player.OnDamage(attackDamage))
                    yield break; // Stop if player is destroyed
            }
            
            // Wait until next attack
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    void OnParticleCollision(GameObject other) {
        if (other.gameObject.CompareTag("Particle") ) {
            OnDamage(20);
        }
    }

	void OnDestroy() {
        enemyWalk.LeaveState();
        enemyIdle.LeaveState();
        waveManager.spawnedEnemies.Remove(gameObject);
    }
}
