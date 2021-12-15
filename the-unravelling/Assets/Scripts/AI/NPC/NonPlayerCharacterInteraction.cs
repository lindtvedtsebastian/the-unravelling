
    

using UnityEngine;

public class NonPlayerCharacterInteraction : State {
   
    private Vector3 DirVec;
    private Transform trans;
    
    private Vector2 initPos;
    

    private DialogueTrigger _dialogueTrigger;

    private Animator _animator;
 
    
    public override void EnterState(StateManager stateManager) {
        
        //Get transform
        trans = GetComponent<Transform>();

        //Set position
        initPos = trans.position;
        
        //Set direction
        DirVec = Vector3.zero;

        //Set animation
        _animator = gameObject.GetComponent<Animator>();
        _animator.Play("Idle");
        //Get dialogue trigger 
        _dialogueTrigger = gameObject.GetComponentInChildren<DialogueTrigger>();
        _stateManager = stateManager;
        
        
        
    }

    public override void DoState() {
        if (!_dialogueTrigger.inRange) {
            _stateManager.setState(_stateManager.GetComponent<NonPlayerCharacterWalk>());
        }

    }

    public override void LeaveState() {
        throw new System.NotImplementedException();
    }
}
