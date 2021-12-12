using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
	private World _world;

	[SerializeField] private Wave[] waves;
    [SerializeField] private bool[] completedWaves;
    [SerializeField] private GameObject enemyContainer;
    private Wave currentWave;
	
    public List<GameObject> spawnedEnemies;

    private bool newWave;

    private int waveIndex;
    private int waveEnemyIndex;
	[SerializeField]
    private int remainingWaveEnemyCount;

    // Start is called before the first frame update
    void Start() {
	    _world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().world;
        spawnedEnemies = new List<GameObject>();
        completedWaves = new bool[waves.Length];

        waveIndex = -1;
        UpdateWaveManagerValues();
    }

    // Update is called once per frame
    void Update() {
        if (_world.state.stateOfDay == CycleState.NIGHT && !completedWaves[waveIndex]) {
            DoWaveAction();
            StartCoroutine(spawnDelay());
        } else
			UpdateWaveManagerValues();
    }

	void DoWaveAction() {
        if (spawnedEnemies.Count < currentWave.maxConcurrentEnemies) {
            WaveEnemy waveEnemy = currentWave.waveEnemies[waveEnemyIndex];
			
			GameObject newEnemy = Instantiate(original: waveEnemy.enemy,
											  position: spawnPosition(),
											  rotation: Quaternion.identity,
											  parent: enemyContainer.transform);
            spawnedEnemies.Add(newEnemy);
            remainingWaveEnemyCount--;

            if (remainingWaveEnemyCount <= 0) {
				if (waveEnemyIndex >= currentWave.waveEnemies.Length-1) {
                    waveEnemyIndex = 0;
                    completedWaves[waveIndex] = true;
                } else {
					waveEnemyIndex++;
                    remainingWaveEnemyCount = currentWave.waveEnemies[waveEnemyIndex].count;
                }
            }
		}
    }

	void UpdateWaveManagerValues() {
		if (waveIndex != _world.state.currentGameDay) {
            waveIndex = _world.state.currentGameDay;
            waveEnemyIndex = 0;
            currentWave = waves[waveIndex];
            remainingWaveEnemyCount = currentWave.waveEnemies[waveEnemyIndex].count;
        }
	} 


	Vector3 spawnPosition() {
		switch (currentWave.waveDirection) {
			case AttackDirection.NORTH: return new Vector3(Random.Range(0,_world.size-1),_world.size-1,0);
			case AttackDirection.SOUTH: return new Vector3(Random.Range(0,_world.size-1),0,0);
			case AttackDirection.WEST:  return new Vector3(0,Random.Range(0,_world.size-1),0);
			default:                    return new Vector3(_world.size-1,Random.Range(0,_world.size-1),0);
        }
	}

	IEnumerator spawnDelay() {
        yield return new WaitForSeconds(currentWave.spawnInterval);
    }
}
