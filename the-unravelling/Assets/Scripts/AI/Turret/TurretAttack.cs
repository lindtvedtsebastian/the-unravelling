using System;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = Unity.Mathematics.quaternion;

public class TurretAttack : State {
    
    private Rigidbody2D _bowBody;
    private Vector3 _bowPosition;
    private ParticleSystem _particleSystem;
	private ParticleSystem.MainModule _particleMain;

    /// <summary>
    /// The state "constructor"
    /// </summary>
    /// <param name="stateManager"></param>
    public override void EnterState(StateManager stateManager) {
        _stateManager = stateManager;
        _bowBody = _stateManager.GetComponent<TurretAI>().bow.GetComponent<Rigidbody2D>();
        _bowPosition = _stateManager.GetComponent<TurretAI>().bow.transform.position;
        _particleSystem = _stateManager.GetComponent<TurretAI>().particleSystem;
        _particleMain = _particleSystem.main;
        _particleMain.startRotation3D = true;
        _particleSystem.Play();
    }
    
    /// <summary>
    /// The state "update"
    /// </summary>
    public override void DoState() {
        TurretAnimation();
    }

    public override void LeaveState() {
        throw new System.NotImplementedException();
    }

    private void TurretAnimation() {
        if (_stateManager.GetComponent<TurretAI>().targetList.FirstOrDefault() != null) {
            Vector3 targetPosition = 
                _stateManager.GetComponent<TurretAI>().targetList.First().gameObject.transform.position;
            
            Vector3 directionToTarget = targetPosition - _bowPosition;

            float offset = -90f;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;


            _bowBody.transform.rotation = UnityEngine.Quaternion.Euler(Vector3.forward * (angle + offset));
            _particleMain.startRotationX = _bowBody.transform.rotation.x;
            _particleMain.startRotationY = _bowBody.transform.rotation.y;
        }
    }
}
