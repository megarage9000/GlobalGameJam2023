using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{

    public EnemyMovement movementScript;
    public AStarSearch searchScript;

    public string playerTag;
    public string TrunkTag;
    public string generatorTag;

    private void Awake() {
        GameObject target = GameObject.FindWithTag(playerTag);
        GameObject trunk = GameObject.FindWithTag(TrunkTag);
        MapGeneration generationScript = GameObject.FindWithTag(generatorTag).GetComponent<MapGeneration>();

        movementScript.target = Random.Range(0, 2) == 1 ? target : trunk;
        searchScript.map = generationScript;
    }
}
