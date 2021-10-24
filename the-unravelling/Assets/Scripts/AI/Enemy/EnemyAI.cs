
public class EnemyAI : StateManager
{
    private EnemyWalk enemyWalk;

    // Start is called before the first frame update
    void Start() {
        enemyWalk = gameObject.AddComponent(typeof(EnemyWalk)) as EnemyWalk;
        availableStates.Add((int) states.enemyWalk,enemyWalk);
        currentState = availableStates[(int) states.enemyWalk];
        currentState.EnterState(this);
    }

	public enum states {
		enemyWalk = 0,
		enemyAttack = 1
	}
}
