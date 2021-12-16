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
        //Add the different state-scripts as component
        NPCWalk = gameObject.AddComponent(typeof(NonPlayerCharacterWalk)) as NonPlayerCharacterWalk;
        NPCInteraction = gameObject.AddComponent(typeof(NonPlayerCharacterInteraction)) as NonPlayerCharacterInteraction;
                
        //Set and enter state.
        setState(NPCWalk);
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.DoState();
    }
    
}
