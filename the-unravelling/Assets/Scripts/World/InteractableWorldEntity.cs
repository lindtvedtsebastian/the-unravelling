using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable World Entity", menuName = "Scriptable Objects/World/Interactable World Entity")]
public class InteractableWorldEntity : WorldEntity {

    public float maxHealth;    
    public int minResourcesReturned;  
    public int maxResourcesReturned; 
    
    
    
    
    /// <summary>
    ///     Fetches max/min range of resources in a single tree and calculates
    ///     how many this tree should give.
    /// </summary>
    /// <returns>Number of resources</returns>
    public int GetResourceAmount() {
        return Random.Range(minResourcesReturned, maxResourcesReturned);
    }
}
