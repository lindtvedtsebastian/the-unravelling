using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
	[SerializeField]
	private Wave[] waves;
    [SerializeField]
    private GameObject enemyContainer;
    private Wave currentWave;
    private List<GameObject> spawnedEnemies;
    private int mapSize;

    // Start is called before the first frame update
    void Start() {
        mapSize = GameData.Get.world.worldSize;
        spawnedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
		if (currentWave == null) {
            GetCurrentWave();
        }
		DoWave();
        if (GameData.Get.world.state.stateOfDay == CycleState.NIGHT) {
            DoWave();
        }
    }

	void DoWave() {
		if (spawnedEnemies.Count <= currentWave.maxConcurrentEnemies) {
			foreach (Wave wave in waves) {
				foreach (WaveEnemy waveEnemy in wave.waveEnemies) {
					while (waveEnemy.count > 0) {
                        Debug.Log("Enemy spawned");
                        GameObject newEnemy = Instantiate(original: waveEnemy.enemy,
														  position: spawnPosition(),
														  rotation: Quaternion.identity,
                                                          parent: enemyContainer.transform);
                        waveEnemy.count--;
                        spawnedEnemies.Add(newEnemy);
                        spawnDelay();
                    }
				}
			}
		}
    }

	void GetCurrentWave() {
        currentWave = waves[GameData.Get.world.state.currentGameDay];
	}

	Vector3 spawnPosition() {
		switch (currentWave.waveDirection) {
			case AttackDirection.NORTH: return new Vector3(Random.Range(0,mapSize-1),mapSize-1,0);
			case AttackDirection.SOUTH: return new Vector3(Random.Range(0,mapSize-1),0,0);
			case AttackDirection.WEST:  return new Vector3(0,Random.Range(0,mapSize-1),0);
			default:                    return new Vector3(mapSize-1,Random.Range(0,mapSize-1),0);
        }
	}

	IEnumerator spawnDelay() {
        yield return new WaitForSeconds(currentWave.spawnInterval);
    }
}
