using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Handles enemy movement
/// </summary>
public class EnemyWalk : State 
{
    private NativeList<PathPart> _resultPath;
    public GameObject _player;

    private float speed = 3f;
    private float proximityRange = 0.5f;

    /// <summary>
    /// All necessary preparations before "do"ing the state 
    /// </summary>
    /// <param name="stateManager">This state's manager</param>
    public override void EnterState(StateManager stateManager) {
        _resultPath = new NativeList<PathPart>(Allocator.Persistent);

        _player = GameObject.FindGameObjectWithTag("Player");
        _stateManager = stateManager;
    }

    /// <summary>
    /// The state "update" loop 
    /// </summary>
    public override void DoState() {
		if (distanceTo(_player.transform.position) > proximityRange) {
        // If no path exists, calculate one
        if (_resultPath.Length <= 0)
            CalculatePath();

        Move();
		}
    }

    /// <summary>
    /// Prepares the State for exit
    /// </summary>
    public override void LeaveState() {
        _resultPath.Dispose();
    }

    /// <summary>
    /// Calculates a new pathfinding path
    /// </summary>
    private void CalculatePath() {
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 playerPos = _player.transform.position;

        int2 startPos = new int2(intRound(enemyPos.x), intRound(enemyPos.y));
        int2 endPos = new int2(intRound(playerPos.x), intRound(playerPos.y));

        _resultPath.Clear();
        Pathfinding pathfinding = new Pathfinding(startPos, endPos, _resultPath);
	}

    /// <summary>
    /// Moves along the calculated path
    /// </summary>
    private void Move() {
		if (_resultPath.Length > 0) {
            PathPart currentWaypoint = _resultPath[_resultPath.Length - 1];
            Vector3 target = new Vector3(currentWaypoint.x, currentWaypoint.y, 0);

            if (currentWaypoint.mustBeDestroyed) {
				// Destroy structure
			} else {
                moveTowards(target);
                if (distanceTo(target) < proximityRange)
					_resultPath.RemoveAt(_resultPath.Length - 1);
            }
        }
	}

    /// <summary>
    /// Helper function for rounding a float to the closest int 
    /// </summary>
    /// <param name="toBeRounded">The float to be rounded</param>
    /// <returns>The provided float converted to the closest int</returns>
    int intRound(float toBeRounded) {
        return (int) Mathf.Round(toBeRounded);
    }


    /// <summary>
    /// Moves this gameObject towards a given vector3 target location 
    /// </summary>
    /// <param name="target">The Vector3 point of which to move towards</param>
    void moveTowards(Vector3 target) {
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
															target,
															speed * Time.deltaTime);
	}

    /// <summary>
    /// Calculates the distance from this gameObject to a given target 
    /// </summary>
    /// <param name="target">The Vector3 of which to calculate the distance to</param>
    /// <returns>The distance (a float number) between this gameObject and the target</returns>
    float distanceTo(Vector3 target) {
        return Vector3.Distance(gameObject.transform.position, target);
    }
}
