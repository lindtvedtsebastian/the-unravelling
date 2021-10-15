using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : State 
{
    private State _state;
    
    public override void EnterState(State state) {
        Debug.Log("EnemyWalk state entered");
        _state = state;
        this.DoState();
    }
    
    public override void DoState() {
        // Do pathfinding 
    }

    public override void LeaveState() {
        throw new System.NotImplementedException();
    }
}
