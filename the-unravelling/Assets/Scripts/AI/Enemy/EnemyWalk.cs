using System.Collections;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyWalk : State 
{
    private StateManager _stateManager;
    private NativeList<PathPart> resultPath;

    public override void EnterState(StateManager stateManager) {
        resultPath = new NativeList<PathPart>(Allocator.Persistent);
        _stateManager = stateManager;
        _stateManager.currentState.DoState();
    }
    
    public override void DoState() {
        Vector3 enemyPos = gameObject.transform.position;
        int2 startPos = new int2((int) Mathf.Floor(enemyPos.x), ((int) Mathf.Floor(enemyPos.y)));
        Pathfinding pathfinding = new Pathfinding(startPos, new int2(25,25), resultPath);
        _stateManager.currentState.LeaveState();
    }

    public override void LeaveState() {
        resultPath.Dispose();
        StartCoroutine(Move());
        _stateManager.currentState = _stateManager.availableStates[(int) EnemyAI.states.enemyWalk];
        _stateManager.currentState.EnterState(_stateManager);
    }

	IEnumerator Move() {
		yield return new WaitForSeconds(3.0f);
	}
}
