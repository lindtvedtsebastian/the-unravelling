using UnityEngine;

public class EnemyIdle : State {
    GameObject _player;

    /// <summary>
    /// The state "constructor"
    /// </summary>
    /// <param name="stateManager">The statemanager</param>
    public override void EnterState(StateManager stateManager) {
        _player = GameObject.FindGameObjectWithTag("Player");
        _stateManager = stateManager;
    }

    /// <summary>
    /// The state "update"
    /// </summary>
    public override void DoState() {
		if (Vector3.Distance(gameObject.transform.position, _player.transform.position) > 3f)
            _stateManager.setState(_stateManager.GetComponent<EnemyWalk>());
    }

    /// <summary>
    /// The state "destructor"
    /// </summary>
    public override void LeaveState() {
    }

}
