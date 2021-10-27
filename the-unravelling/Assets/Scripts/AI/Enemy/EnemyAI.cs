using UnityEngine;

public class EnemyAI : StateManager {
    private WaveManager waveManager;
    public EnemyWalk enemyWalk;
    public EnemyIdle enemyIdle;

    // Start is called before the first frame update
    void Start() {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();

        enemyWalk = gameObject.AddComponent(typeof(EnemyWalk)) as EnemyWalk;
        enemyIdle = gameObject.AddComponent(typeof(EnemyIdle)) as EnemyIdle;

        setState(enemyWalk);
        currentState.EnterState(this);
    }

	void Update() {
        currentState.DoState();
    }

	void OnDestroy() {
        enemyWalk.LeaveState();
        enemyIdle.LeaveState();
        waveManager.spawnedEnemies.Remove(gameObject);
    }
}
