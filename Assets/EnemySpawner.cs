using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private void Awake() {
        
    }
    IEnumerator SpawningRoutine(int numberOfEnemies, int spawnRatePerSecond) {
        for (int i = 0; i < numberOfEnemies; i++) {


            yield return new WaitForSeconds(1f / spawnRatePerSecond);
        }

    }
}
