using UnityEngine;

public class EnemyAI : StateManager {

    // Health bar that appears as the unit takes damage.
    [SerializeField] private GameObject healthBar;
    [SerializeField] private int health;

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


    void OnParticleCollision(GameObject other) {
        Debug.Log("collision");
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
