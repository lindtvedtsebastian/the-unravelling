
public class EnemyAI : StateManager
{
    private EnemyWalk enemyWalk;
    private EnemyIdle enemyIdle;

    // Start is called before the first frame update
    void Start() {
        enemyWalk = gameObject.AddComponent(typeof(EnemyWalk)) as EnemyWalk;
        enemyIdle = gameObject.AddComponent(typeof(EnemyIdle)) as EnemyIdle;

        setState(enemyWalk);
        currentState.EnterState(this);
    }

	void Update() {
        currentState.DoState();
    }
}
