using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public UnityEvent EnemiesCleared;

    private int numEnemies;
    


    IEnumerator SpawningRoutine(int numberOfEnemies, int spawnRatePerSecond, Transform new_transform) {

        numEnemies = numberOfEnemies;

        for (int i = 0; i < numberOfEnemies; i++) {
            Instantiate(EnemyPrefab, transform.position, transform.rotation);
            transform.position = new_transform.position;
            transform.rotation = new_transform.rotation;
            yield return new WaitForSeconds(1f / spawnRatePerSecond);
        }
    }

    void onEnemyKilled() {
        numEnemies--;
        if(numEnemies <= 0) {
            EnemiesCleared?.Invoke();
        }
    }
}
