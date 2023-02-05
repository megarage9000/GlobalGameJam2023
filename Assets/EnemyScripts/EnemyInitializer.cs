using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{

    public EnemyMovement movementScript;
    public AStarSearch searchScript;

    public string playerTag;
    public string generatorTag;


    private void Awake() {
        GameObject target = GameObject.FindWithTag(playerTag);
        MapGeneration generationScript = GameObject.FindWithTag(generatorTag).GetComponent<MapGeneration>();

        movementScript.target = target;
        searchScript.map = generationScript;
    }
}
