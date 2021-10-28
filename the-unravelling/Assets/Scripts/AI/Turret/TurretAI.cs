using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Transforms;
using UnityEngine;

public class TurretAI : StateManager {
    
    // Variables for controlling turret animation
    [SerializeField] public GameObject bow;
    public float rotationSpeed = 25.0f;
    
    // ParticleSystem that controls the turrets shooting
    public ParticleSystem particleSystem;

    // List that holds the enemies within vision range
    public List<GameObject> targetList;

    // The different states of the turret.
    private TurretIdle _turretIdle;
    private TurretAttack _turretAttack;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start() {

        targetList = new List<GameObject>();
        
        _turretIdle = gameObject.AddComponent(typeof(TurretIdle)) as TurretIdle;
        _turretAttack = gameObject.AddComponent(typeof(TurretAttack)) as TurretAttack;
        
        setState(_turretIdle);
        currentState.EnterState(this);
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update() {
        currentState.DoState();

        if (targetList.Count.Equals(0)) {
            setState(_turretIdle);
            currentState.EnterState(this);
        }
        else {
            setState(_turretAttack);
            currentState.EnterState(this);
        }
    }
    
    /// <summary>
    /// Function for finding "Enemy" GameObjects within the Turrets vision range.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy") && !targetList.Contains(other.gameObject)) {
            targetList.Add(other.gameObject);
        }
    }
    
    /// <summary>
    /// Function for handling "Enemy" GameObjects leaving the Turrets vision range.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other) {
        if (targetList.Contains(other.gameObject)) {
            targetList.Remove(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other) {
        if (other.gameObject.CompareTag("Enemy")) {
            
        }
    }
}
