using UnityEngine;

public class NonPlayerCharacterWalk : State {

    private Vector3 DirVec;
    private Transform trans;
    public float movementSpeed = 2.0f;
    private Vector2 initPos;
     
    private float stuckTime;
    private float stuckThreshold = 2f;
    private Vector2 prevPos;

    private float moveTime;
    private bool isNPCStuck;

    private SpriteRenderer _spriteRenderer;
    private DialogueTrigger _dialogueTrigger;

    private Animator _animator;
    
    /// <summary>
    /// Preparation of state. 
    /// </summary>
    /// <param name="stateManager">The statemanager for NPC</param>
    public override void EnterState(StateManager stateManager) {
        _stateManager = stateManager;
        
        //Set position and direction of object 
        trans = GetComponent<Transform>();
        initPos = prevPos = trans.position;
        DirVec = Vector3.left;
        
        //Set animation 
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator.Play("Walk");
        _spriteRenderer.flipX = true;
        
        //Get dialogue trigger 
        _dialogueTrigger = gameObject.GetComponentInChildren<DialogueTrigger>();
        
        MoveNPC();
    }

    /// <summary>
    /// Doing the state.
    /// </summary>
    public override void DoState() {
        stuckTime -= Time.deltaTime;
        if (stuckTime <= 0) {
            
            //If gameobject is stuck move it down.
            if (isStuck()) { 
                trans.position += Vector3.down;
            }
            
            prevPos = gameObject.transform.position;
            stuckTime = stuckThreshold;
        }
        
        
        CheckBounds();
        MoveNPC();
        if (_dialogueTrigger.inRange) {
            _stateManager.setState(_stateManager.GetComponent<NonPlayerCharacterInteraction>());
        }
    }

    /// <summary>
    /// Cleanup after state.
    /// </summary>
    public override void LeaveState() {
        Debug.Log("No cleanup needed");
    }


    /// <summary>
    /// Change the position of the NPC 
    /// </summary>
    private void MoveNPC() {
        trans.position += DirVec * movementSpeed * Time.deltaTime;
    }

  
    /// <summary>
    /// Creates bounds for the NPC to move within.
    /// </summary>
    private void CheckBounds() {
        if (trans.position.x > initPos.x + 3.0f) {
            DirVec = Vector3.left;
            _spriteRenderer.flipX = true;
        }
        else if (trans.position.x < initPos.x - 3.0f) {
            DirVec = Vector3.right;
            _spriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// Checks if object is stuck 
    /// </summary>
    /// <returns>true if stuck</returns>
    private bool isStuck() {
        Vector2 currPos = gameObject.transform.position;
        if (Mathf.Abs(prevPos.x - currPos.x) < .09f)
            return true;
        return false;
    }
}
