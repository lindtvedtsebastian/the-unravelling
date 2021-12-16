using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// Handles turret attack state
/// </summary>
public class TurretAttack : State {
    
    private Rigidbody2D _bowBody;
    private Vector3 _bowPosition;
    private ParticleSystem _particleSystem;
	private ParticleSystem.MainModule _particleMain;
    private float attackTimer;
    private float attackThreshold = 2f;

    /// <summary>
    /// The state "constructor" method
    /// </summary>
    /// <param name="stateManager">This states manager</param>
    public override void EnterState(StateManager stateManager) {
        _stateManager = stateManager;
        _bowBody = _stateManager.GetComponent<TurretAI>().bow.GetComponent<Rigidbody2D>();
        _bowPosition = _stateManager.GetComponent<TurretAI>().bow.transform.position;
        _particleSystem = _stateManager.GetComponent<TurretAI>().particleSystem;
        _particleMain = _particleSystem.main;
        _particleMain.startRotation3D = true;
    }
    
    /// <summary>
    /// The state "update" method
    /// </summary>
    /// <see cref="TurretAnimation()"/>
    public override void DoState() {
        TurretAnimation();

        attackTimer += Time.deltaTime;
		if (attackTimer >= attackThreshold) {
            attackTimer = 0;
            _particleSystem.Play();
		}
    }

    /// <summary>
    /// The state "destructor" method
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public override void LeaveState() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Handles the turrets targeting system where the turret will target lock on an enemy based on the calculated rotation
    /// </summary>
    private void TurretAnimation() {
        
        // Defensive check for an empty list
        if (_stateManager.GetComponent<TurretAI>().targetList.FirstOrDefault() != null) {
            Vector3 targetPosition = 
                _stateManager.GetComponent<TurretAI>().targetList.First().gameObject.transform.position;
            
            // Calculating vector to target
            Vector3 directionToTarget = targetPosition - _bowPosition;
            
            // Calculating angle to target in degrees
            float offset = 90f;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
            float angleInDeg = angle * Mathf.Rad2Deg;

            
            // Rotates turret towards target
            _bowBody.transform.rotation = UnityEngine.Quaternion.Euler(Vector3.forward * (angleInDeg - offset));
			
            _particleMain.startRotationX = 0;
            _particleMain.startRotationY = 0;
            _particleMain.startRotationZ = (angle - (offset * Mathf.Deg2Rad)) * -1;

        }
    }
}
 
