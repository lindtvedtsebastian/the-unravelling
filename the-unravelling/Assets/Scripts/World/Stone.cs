using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Stone : WorldEntity {
    public enum stoneType {
        Stone,
        IronOre
    };
    public int maxHealth;    
    private int resourcesReturned; //Number of resources returned when mined 
    
}

