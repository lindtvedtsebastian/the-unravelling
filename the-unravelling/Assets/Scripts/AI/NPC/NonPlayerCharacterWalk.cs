




using UnityEngine;
using UnityEngine.EventSystems;

public class NonPlayerCharacterWalk : State {

    private Vector3 DirVec;
    private Transform trans;
    public float movementSpeed = 3.0f;
    private Rigidbody2D rigid;
    public Collider2D boundaries;
    
    public override void EnterState(StateManager stateManager) {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        ChangeDir();
    }

    public override void DoState() {
        MoveNPC();
    }

    public override void LeaveState()
    {
        throw new System.NotImplementedException();
    }


    private void MoveNPC() {
        var tmp = trans.position + DirVec * movementSpeed * Time.deltaTime;
        if(boundaries.bounds.Contains(tmp))
            rigid.MovePosition(tmp);
        else
            ChangeDir();
    }

    /// <summary>
    /// Changes the direction of the NPC
    /// </summary>
    private void ChangeDir() {
        int dir = Random.Range(0, 4);
        switch (dir) {
            case 0:
                DirVec = Vector3.right;
                break;
            case 1:
                DirVec = Vector3.up;
                break;
            case 2:
                DirVec = Vector3.left;
                break;
            case 3:
                DirVec = Vector3.down;
                break;
            default:
                break;
        }
        //Update animations here 
    } 
    
    
}
