using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : State {
    GameObject _player;

    public override void EnterState(StateManager stateManager) {
        _player = GameObject.FindGameObjectWithTag("Player");
        _stateManager = stateManager;
    }

    public override void DoState() {
		if (Vector3.Distance(gameObject.transform.position, _player.transform.position) > 3f)
            _stateManager.setState(_stateManager.GetComponent<EnemyWalk>());
    }


    public override void LeaveState() {
        throw new System.NotImplementedException();
    }

}
