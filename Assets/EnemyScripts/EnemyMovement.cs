using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public int UpdatesPerSecond = 2;
    public GameObject target;

    AStarSearch searcherScript;
    List<Vector3> path;
    Vector3 next_goal;
    float y_displacement;
    bool canMove = false;

    IEnumerator SearchRoutine() {
        while(true) {
            path = searcherScript.BeginSearch(transform.position, target.transform.position);
            if(path.Count > 0) {
                next_goal = path[0];
                next_goal.y += y_displacement;
            }
            yield return new WaitForSeconds(1f/UpdatesPerSecond);
        }
    }

    private void Awake() {
        searcherScript = GetComponent<AStarSearch>();
        y_displacement = 0.0f;
        canMove = true;
    }

    void Start()
    {
        StartPathFinding();
    }

    public void StartPathFinding() {
        StartCoroutine(SearchRoutine());
        canMove = true;
    }    

    public void StopPathFinding() {
        searcherScript.StopSearch();
        StopCoroutine(SearchRoutine());
        canMove = false;
    }

    private void Search() {
        float move = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, next_goal, move);
        transform.LookAt(next_goal);
    }

    private void Update() {
        if(canMove) {
            Search();
        }
    }
}
