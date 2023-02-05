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
        y_displacement = transform.position.y + 0.1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SearchRoutine());
    }

    // Run in Update
    private void Search() {
        float move = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, next_goal, move);
    }

    private void Update() {
        Search();
    }

    private void FixedUpdate() {
        
    }
}
