




using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
    
    public override void EnterState(StateManager stateManager) {
        _stateManager = stateManager;
        trans = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        initPos = prevPos = trans.position;
        DirVec = Vector3.left;
        
        //Set animation 
        _animator = gameObject.GetComponent<Animator>();
        _animator.Play("Walk");
        _spriteRenderer.flipX = true;
        
        //Get dialogue trigger 
        _dialogueTrigger = gameObject.GetComponentInChildren<DialogueTrigger>();
        MoveNPC();
    }

    public override void DoState() {
        stuckTime -= Time.deltaTime;
 
        
        if (stuckTime <= 0) {
            
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

    public override void LeaveState() {
        //Nothing to clean up
        Debug.Log("No cleanup needed");
    }


    private void MoveNPC() {
        trans.position += DirVec * movementSpeed * Time.deltaTime;
    }

  
    /// <summary>
    /// Creates bounds for the NPC to move within.
    /// </summary>
    private void CheckBounds() {
        if (trans.position.x > initPos.x + 3.0f)
        {
            DirVec = Vector3.left;
            _spriteRenderer.flipX = true;
        }
        else if (trans.position.x < initPos.x - 3.0f)
        {
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
    
    
    /// <summary>
    /// Start interaction state on collision 
    /// </summary>
    /// <param name="other">Other gameobject</param>
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered");
        if (other.gameObject.tag == "Player") {
            _stateManager.setState(_stateManager.GetComponent<NonPlayerCharacterInteraction>());
        }
    }
    
    
}
