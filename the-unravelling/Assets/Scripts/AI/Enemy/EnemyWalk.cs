using System.Collections;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyWalk : State 
{
    private StateManager _stateManager;
    private NativeList<PathPart> _resultPath;
    public GameObject _player;

    public override void EnterState(StateManager stateManager) {
        _resultPath = new NativeList<PathPart>(Allocator.Persistent);

        _player = GameObject.FindGameObjectWithTag("Player");
        _stateManager = stateManager;
    }
    
    public override void DoState() {
		if (Vector3.Distance(gameObject.transform.position, _player.transform.position) > 0.5f) {
        // If no path exists, calculate one
        if (_resultPath.Length <= 0)
            CalculatePath();

        Move();
		}
    }

    public override void LeaveState() {
        _resultPath.Dispose();
    }

    private void CalculatePath() {
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 playerPos = _player.transform.position;

        int2 startPos = new int2((int) Mathf.Floor(enemyPos.x), ((int) Mathf.Floor(enemyPos.y)));
        int2 endPos = new int2((int)Mathf.Floor(playerPos.x), ((int)Mathf.Floor(playerPos.y)));

        Pathfinding pathfinding = new Pathfinding(startPos, endPos, _resultPath);
	}

    private void Move() {
		if (_resultPath.Length > 0) {
            PathPart currentWaypoint = _resultPath[_resultPath.Length - 1];

            if (currentWaypoint.mustBeDestroyed) {
				// Destroy structure
			} else {
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
																	new Vector3(currentWaypoint.x, currentWaypoint.y,0),
																	3f * Time.deltaTime);
				if (Vector3.Distance(gameObject.transform.position, new Vector3(currentWaypoint.x, currentWaypoint.y,0)) < 0.5f)
					_resultPath.RemoveAt(_resultPath.Length - 1);
            }
        }
	}

}
