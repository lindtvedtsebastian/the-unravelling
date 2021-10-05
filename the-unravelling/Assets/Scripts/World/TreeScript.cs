using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class TreeScript : MonoBehaviour {
    public TreeScriptableObject sharedData;

    private void Start() {
      
    }

    //Run when game-object is destroyed
    private void OnDestroy() {
        Debug.Log("Tree object destroyed, gave " + GetResource() + " wood!" );
        Destroy(this);
    }

    /// <summary>
    ///     Fetches max/min range of resources in a single tree and calculates
    ///     how many this tree should give.
    /// </summary>
    /// <returns>Number of resources</returns>
    private int GetResource() {
        var rnd = new Random();
        return rnd.Next(sharedData.minResourcesReturned, sharedData.maxResourcesReturned);
    }
    
}
