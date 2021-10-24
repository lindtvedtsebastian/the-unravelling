using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyWalk : State 
{
    private State _state;
    private NativeList<PathPart> resultPath;

    public override void EnterState(State state) {
        resultPath = new NativeList<PathPart>(Allocator.Persistent);
        _state = state;
        _state.DoState();
    }
    
    public override void DoState() {
        Vector3 enemyPos = gameObject.transform.position;
        int2 startPos = new int2((int) Mathf.Floor(enemyPos.x), ((int) Mathf.Floor(enemyPos.y)));
        Pathfinding pathfinding = new Pathfinding(startPos, new int2(25,25), resultPath);
        _state.LeaveState();
    }

    public override void LeaveState() {
        resultPath.Dispose();
    }
}
