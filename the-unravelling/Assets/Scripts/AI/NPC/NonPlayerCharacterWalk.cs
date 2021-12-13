




using UnityEngine;

public class NonPlayerCharacterWalk : State {
    
    
    public float moveSpeed ;
    public Vector3 dir;
    public float turnSpeed;
    float targetAngle;
    Vector3 currentPos;
    bool play=true;
    Vector3 direction;

    
    public override void EnterState(StateManager stateManager) {
        //Get terrain
        var WM = GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>();
        var terrain = WM.world.terrain;
        

        
        
        dir = Vector3.up;
        direction = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-4.0f, 4.0f), 0);
    }

    public override void DoState()
    {
        throw new System.NotImplementedException();
    }

    public override void LeaveState()
    {
        throw new System.NotImplementedException();
    }


    private void ChangeDir()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }

    } 
}
