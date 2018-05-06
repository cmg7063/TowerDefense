using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public enum SpawnState {
        Spawning, 
        Waiting, 
        Counting
    };

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy1;
        public Transform enemy2;
        public Transform enemy3;
        public Transform enemy4;
        public Transform enemy5;
        public Transform enemy6;
        public Transform enemy7;
        public Transform enemy8;
        public Transform enemy9;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
	private int totalWaves = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

	// Use this for initialization
	void Start () {
        waveCountdown = timeBetweenWaves;

        if (spawnPoints.Length == 0) {
            Debug.Log("No spawn points referenced.");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (state == SpawnState.Waiting) {
            if(!EnemyIsAlive()) {
                WaveCompleted();
            } else {
                return;
            }
        }

		if (waveCountdown <= 0) {
            if(state != SpawnState.Spawning) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
	}

    void WaveCompleted() {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;
		totalWaves++;

        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("All waves complete. Looping...");
        } else {
            nextWave++;
        }
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f) {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        state = SpawnState.Spawning;
		float enemySpawnMod = Mathf.Floor((totalWaves / 3) + 1);

		for(int i = 0; i < _wave.count * enemySpawnMod; i++) {
			float variation = Random.Range (0.0f, 100.0f);
			int enemyType = Random.Range (0, 3);

			// variation 2 = ranged
			// variation 3 = kamikaze
			float variation3Rate = totalWaves * 2f;
			float variation2Rate = totalWaves * 3f;

			Debug.Log (variation + ", " + variation3Rate + ", " + variation2Rate);
			if (variation <= variation3Rate) {
				if (enemyType == 0) {
					SpawnEnemy (_wave.enemy7);
					yield return new WaitForSeconds (1f / _wave.rate);
				} else if (enemyType == 1) {
					SpawnEnemy (_wave.enemy8);
					yield return new WaitForSeconds (1f / _wave.rate);
				} else {
					SpawnEnemy (_wave.enemy9);
					yield return new WaitForSeconds (1f / _wave.rate);
				}
			} else if (variation < variation2Rate + variation3Rate) {
				if (enemyType == 0) {
					SpawnEnemy (_wave.enemy3);
					yield return new WaitForSeconds (1f / _wave.rate);
				} else if (enemyType == 1) {
					SpawnEnemy (_wave.enemy4);
					yield return new WaitForSeconds (1f / _wave.rate);
				} else {
					SpawnEnemy (_wave.enemy5);
					yield return new WaitForSeconds (1f / _wave.rate);
				}
			} else {
				if (enemyType == 0) {
					SpawnEnemy (_wave.enemy1);
					yield return new WaitForSeconds (1f / _wave.rate);
				} else if (enemyType == 1) {
					SpawnEnemy (_wave.enemy2);
					yield return new WaitForSeconds (1f / _wave.rate);
				} else {
					SpawnEnemy (_wave.enemy3);
					yield return new WaitForSeconds (1f / _wave.rate);
				}
			}

            //yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
//        Debug.Log("Spawning Enemy:" + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
