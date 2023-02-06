using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{

    public UnityEvent OnCharacterSpot;
    public UnityEvent OnCharacterLost;

    public string PlayerTag = "Target";
    GameObject PlayerObject;

    private void Awake() {
        PlayerObject = null;
    }

    private void Update() {
        if(PlayerObject) {
            transform.LookAt(PlayerObject.transform.position, Vector3.up);
            // Debug.Log("ATTACKING");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals(PlayerTag)) {
            PlayerObject = other.gameObject;
            OnCharacterSpot.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals(PlayerTag)) {
            PlayerObject = null;
            OnCharacterLost.Invoke();
        }
    }
}
