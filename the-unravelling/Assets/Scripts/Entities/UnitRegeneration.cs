
using System;
using System.Collections;
using UnityEngine;


/// <summary>
/// A base class for units.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class UnitRegeneration : BaseUnit{
    
    //Is currently regenerating
    private bool isRegenerating;

    //Number of health-points an objects regenerates each cycle
    [SerializeField]private int regenHealth;

    //Time between each regeneration cycle
    [SerializeField] private int regenRate;
    
    protected override void Awake() {
        base.Awake();
        isRegenerating = false;
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
        
        //Regenerate
        while (health < maxHealth) {
            health += regenHealth;
            yield return new WaitForSeconds(regenRate);
        }
        isRegenerating = false;    
            
    }


    /// <summary>
    ///  Calls parent OnDamage and starts regeneration if damage was taken
    /// </summary>
    /// <param name="damage">The amount of damage to inflict on the object</param>
    public override void OnDamage(int damage) {
        base.OnDamage(damage);
        isRegenerating = false;
        if (health < maxHealth) {
            StartCoroutine(RegenHealth());
        }
    }

    /// <summary>
    /// Called when health is zero
    /// </summary>
    protected override void OnDestroy() {
        gameObject.SetActive(false);
    }
    
}