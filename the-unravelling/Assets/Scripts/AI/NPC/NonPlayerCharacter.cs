using System;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;


/// <summary>
/// NPC base class 
/// </summary>
public class NonPlayerCharacter : StateManager {

    public NonPlayerCharacterInteraction NPCInteraction;
    public NonPlayerCharacterWalk NPCWalk;
    
    private void Start() {
        setState(NPCWalk);
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.DoState();
    }
    
}
