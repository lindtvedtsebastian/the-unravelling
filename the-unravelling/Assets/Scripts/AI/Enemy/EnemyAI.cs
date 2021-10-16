using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private State _state;

    // Start is called before the first frame update
    void Start() {
        _state = new EnemyWalk();
        _state.EnterState(_state);
        _state.DoState();
    }
}
