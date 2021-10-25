
public class EnemyAI : StateManager
{
    private EnemyWalk enemyWalk;

    // Start is called before the first frame update
    void Start() {
        enemyWalk = gameObject.AddComponent(typeof(EnemyWalk)) as EnemyWalk;

        currentState = enemyWalk;
        currentState.EnterState(this);
    }

	void Update() {
        currentState.DoState();
    }
}
