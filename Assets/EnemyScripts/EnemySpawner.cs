using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public UnityEvent EnemiesCleared;

    public int Round = 1;

    private int numEnemies;
    
    public void SpawningRoutine(Transform new_transform) {

        
        for (int i = 0; i < numEnemies; i++) {
            GameObject spawned = Instantiate(EnemyPrefab, transform.position, transform.rotation);
            HealthSystem healthSystem = spawned.GetComponent<HealthSystem>();
            if(healthSystem) {
                healthSystem.OnDeath.AddListener(onEnemyKilled);
            }
            transform.position = new_transform.position;
            transform.rotation = new_transform.rotation;
        }
    }

    public void StartNewRound(int round) {

    }

    public void onEnemyKilled() {
        numEnemies--;
        if(numEnemies <= 0) {
            Round++;
        }
    }
}
