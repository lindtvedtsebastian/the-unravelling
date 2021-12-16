using System.Collections.Generic;
using UnityEngine;

public class TurretAI : StateManager {
    
    // Variables for controlling turret animation
    [SerializeField] public GameObject bow;
    public float rotationSpeed = 25.0f;
    
    // ParticleSystem that controls the turrets shooting
    public ParticleSystem particleSystem;

    // List that holds the enemies within vision range
    public List<GameObject> targetList;

    // The different states of the turret
    private TurretIdle _turretIdle;
    private TurretAttack _turretAttack;
    
    void Start() {
        
        targetList = new List<GameObject>();
        
        _turretIdle = gameObject.AddComponent(typeof(TurretIdle)) as TurretIdle;
        _turretAttack = gameObject.AddComponent(typeof(TurretAttack)) as TurretAttack;
        
        setState(_turretIdle);
        currentState.EnterState(this);
    }

    void Update() {
        
        currentState.DoState();
        
        // Determining state based on existing enemies within vision range or not 
        if (targetList.Count.Equals(0)) {
            setState(_turretIdle);
            currentState.EnterState(this);
        }
        else {
            setState(_turretAttack);
            currentState.EnterState(this);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        
        // The collider works as the turrets vision range and handles enemies entering and exiting
        if (other.gameObject.CompareTag("Enemy") && !targetList.Contains(other.gameObject)) {
            targetList.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        
        if (targetList.Contains(other.gameObject)) {
            targetList.Remove(other.gameObject);
        }
    }
}
