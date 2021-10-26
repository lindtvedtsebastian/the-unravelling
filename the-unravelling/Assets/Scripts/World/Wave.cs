using System;
using UnityEngine;

/// <summary>
/// Defines how a wave should be  
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class Wave : ScriptableObject {
    public WaveEnemy[] waveEnemies;
    public AttackDirection waveDirection;
    public int maxConcurrentEnemies;
    public float spawnInterval;
}

/// <summary>
/// The enemy prefab to be spawned, and the amount of that prefab
/// </summary>
[Serializable]
public class WaveEnemy {
    public GameObject enemy;
    public int count;
}

/// <summary>
/// In what direction the enemies of the wave should attack from 
/// </summary>
public enum AttackDirection {
    NORTH,
    SOUTH,
    EAST,
    WEST,
}
