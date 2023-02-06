using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public UnityEvent EnemiesCleared;
    public List<Transform> spawn_locations;
    public int Round = 1;
    public int GrowthRate = 10;
    public float spawnRateIncrease = 0.1f;

    public int MAX_NUM_ENEMIES = 100;
    public float MAX_SPAWN_RATE = 5;

    private int numEnemies;
    private int numSpawners;
    private int currentSpawner;
    private float currentSpawnRate;
    private int minNumEnemiesBeforeNextRound;
    private int killedEnemies;

    private void Awake() {
        numSpawners = spawn_locations.Count;
        currentSpawner = 0;
    }

    public IEnumerator SpawningRoutine(float spawnRate) {
        int numSpawns = numEnemies;
        for (int i = 0; i < numSpawns; i++) {
            GameObject spawned = Instantiate(EnemyPrefab, transform.position, transform.rotation);
            HealthSystem healthSystem = spawned.GetComponent<HealthSystem>();
            if(healthSystem) {
                healthSystem.OnDeath.AddListener(onEnemyKilled);
            }
            ChangeSpawnTransform();
            yield return new WaitForSeconds(1f / spawnRate);
        }
    }

    private void ChangeSpawnTransform() {

        Transform new_transform = spawn_locations[currentSpawner];
        transform.position = new_transform.position;
        transform.rotation = new_transform.rotation;

        currentSpawner = Random.Range(0, numSpawners);
    }

    public void StartNewRound() {
        numEnemies = Round * GrowthRate;
        currentSpawnRate = currentSpawnRate + (Round * spawnRateIncrease);
        numEnemies = Mathf.Min(numEnemies, MAX_NUM_ENEMIES);
        currentSpawnRate = Mathf.Min(currentSpawnRate, MAX_SPAWN_RATE);
        minNumEnemiesBeforeNextRound = Mathf.CeilToInt(numEnemies * 0.10f);
        killedEnemies = 0;
        Debug.Log($" Num Enemies = {numEnemies}, Current spawn rate = {1f/currentSpawnRate}, Round {Round}, minNumEnemies = {minNumEnemiesBeforeNextRound}");
        StartCoroutine(SpawningRoutine(currentSpawnRate));
    }

    public void onEnemyKilled() {
        killedEnemies++;
        Debug.Log($"Enemies left = {numEnemies}");
        if((numEnemies - killedEnemies) <= minNumEnemiesBeforeNextRound) {
            Round++;
            StartNewRound();
        }
    }
}
