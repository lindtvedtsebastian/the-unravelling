using System;
using UnityEngine;

/// <summary>
/// Handles turret idle state
/// </summary>
public class TurretIdle : State {
    
    private float rotationSpeed;
    private Rigidbody2D _bowBody;
    private ParticleSystem _particleSystem;

    /// <summary>
    /// The state "constructor" method
    /// </summary>
    /// <param name="stateManager">This states manager</param>
    public override void EnterState(StateManager stateManager) {
        _stateManager = stateManager;
        rotationSpeed = _stateManager.GetComponent<TurretAI>().rotationSpeed;
        _bowBody = _stateManager.GetComponent<TurretAI>().bow.GetComponent<Rigidbody2D>();
        _particleSystem = _stateManager.GetComponent<TurretAI>().particleSystem;
        _particleSystem.Stop();
    }
    
    /// <summary>
    /// The state "update" method
    /// </summary>
    public override void DoState() {
        TurretAnimation();
    }
    
    /// <summary>
    /// The state "destructor" method
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void LeaveState() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Handles the turrets idle animation in form of a simple rotation
    /// </summary>
    private void TurretAnimation() {
        _bowBody.transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime));
    }
}
