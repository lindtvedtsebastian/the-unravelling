
using System;
using System.Collections;
using UnityEngine;


/// <summary>
/// A base class for units.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Destrucatble : BaseUnit{
   
    //Can regenerate health
    [SerializeField]private bool isHealthRegenerable;
    
    //Is currently regenerating
    private bool isRegenerating;

    //Number of health-points an objects regenerates each cycle
    [SerializeField]private int regenHealth;

    //Time between each regen cycle
    [SerializeField] private int regenRate;
    
    protected override void Awake() {
        base.Awake();
        isRegenerating = false;
    }

    private void Update() {
        //If object has less than max health and is supposed to regenerate 
        if (isHealthRegenerable && health < maxHealth) {
            StartCoroutine(RegenHealth());
            ClampHealth();
        }
    }

    /// <summary>
    ///  Regenerates health 
    /// </summary>
    /// <returns>Delay of regenRate length</returns>
    private IEnumerator RegenHealth() {
        // Add 6 second delay before regeneration starts.
        if (!isRegenerating) {
            isRegenerating = true;
            yield return new WaitForSeconds(6);
        }
        
        while (health < maxHealth) {
            health += regenHealth;
            yield return new WaitForSeconds(regenRate);
        }
    }

    /// <summary>
    /// Restricting number between maxHealth and 0 
    /// </summary>
    private void ClampHealth() {
        if (health >= maxHealth) {
            health = maxHealth;
            isRegenerating = false;
        }
        else if (health < 0) {
            health = 0;
        }
    }

    
    /// <summary>
    /// Called when health is 0
    /// </summary>
    protected override void OnDestroy() {
        gameObject.SetActive(false);
    }
    
}