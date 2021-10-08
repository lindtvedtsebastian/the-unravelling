using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tree Settings")]
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
