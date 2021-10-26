using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class Wave : ScriptableObject {
    public WaveEnemy[] waveEnemies;
    public Direction waveDirection;
}


[Serializable]
public class WaveEnemy {
    public GameObject enemy;
    public int count;
}

public enum Direction {
    NORTH,
    SOUTH,
    EAST,
    WEST,
}
