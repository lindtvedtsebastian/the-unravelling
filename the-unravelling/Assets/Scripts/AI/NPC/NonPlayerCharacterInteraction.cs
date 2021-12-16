
    

using UnityEngine;

/// <summary>
/// Handles interaction/idle state of the NPC.
/// </summary>
public class NonPlayerCharacterInteraction : State {
    
    private DialogueTrigger _dialogueTrigger;
    private Animator _animator;
 
    /// <summary>
    /// Preparation of state. 
    /// </summary>
    /// <param name="stateManager">The statemanager for NPC</param>
    public override void EnterState(StateManager stateManager) {
        //Set animation
        _animator = gameObject.GetComponent<Animator>();
        _animator.Play("Idle");
        
        //Get dialogue trigger 
        _dialogueTrigger = gameObject.GetComponentInChildren<DialogueTrigger>();
        _stateManager = stateManager;
    }

    /// <summary>
    /// Doing the state.
    /// </summary>
    public override void DoState() {
        //Checking if player is in range of NPC
        if (!_dialogueTrigger.inRange) {
            _stateManager.setState(_stateManager.GetComponent<NonPlayerCharacterWalk>());
        }
    }
    
    /// <summary>
    /// Cleanup after state.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void LeaveState() {
        Debug.Log("Nothing to clean up");
    }
}
