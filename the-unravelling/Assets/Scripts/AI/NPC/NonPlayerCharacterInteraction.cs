
    

using UnityEngine;

/// <summary>
/// Handles interaction/idle state of the NPC.
/// </summary>
public class NonPlayerCharacterInteraction : State {
    
<<<<<<< HEAD
    private Vector2 initPos;

=======
>>>>>>> 684a0a26bddd3ba741082f5abd5504644eb6904f
    private DialogueTrigger _dialogueTrigger;
    private Animator _animator;
 
<<<<<<< HEAD
=======
    /// <summary>
    /// Preparation of state. 
    /// </summary>
    /// <param name="stateManager">The statemanager for NPC</param>
>>>>>>> 684a0a26bddd3ba741082f5abd5504644eb6904f
    public override void EnterState(StateManager stateManager) {
        //Set animation
        _animator = gameObject.GetComponent<Animator>();
        _animator.Play("Idle");
        
        //Get dialogue trigger 
        _dialogueTrigger = gameObject.GetComponentInChildren<DialogueTrigger>();
<<<<<<< HEAD
        _stateManager = stateManager; 
=======
        _stateManager = stateManager;
>>>>>>> 684a0a26bddd3ba741082f5abd5504644eb6904f
    }

    /// <summary>
    /// Doing the state.
    /// </summary>
    public override void DoState() {
        //Checking if player is in range og NPC
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
