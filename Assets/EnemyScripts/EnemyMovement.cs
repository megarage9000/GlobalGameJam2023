using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public int UpdatesPerSecond = 2;
    public GameObject target;

    Rigidbody rb;
    AStarSearch searcherScript;
    float y_displacement;
    List<Vector3> path;
    Vector3 current_goal;
    Vector3 next_goal;

    IEnumerator SearchRoutine() {
        while(true) {
            path = searcherScript.BeginSearch(transform.position, target.transform.position);
            if(path.Count > 0) {
                next_goal = path[0];
                transform.position = path[0];
                Debug.Log($"Going to position {path[0]}");
            }
            yield return new WaitForSeconds(1f/UpdatesPerSecond);
        }
    }

    void DetermineNextPosition() {
        path = searcherScript.BeginSearch(transform.position, target.transform.position);
        if (path.Count > 0) {
            current_goal = path[0];
        }
    }

    private void Awake() {
        searcherScript = GetComponent<AStarSearch>();
        rb = GetComponent<Rigidbody>();
        y_displacement = transform.position.y + 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SearchRoutine());
    }
}
