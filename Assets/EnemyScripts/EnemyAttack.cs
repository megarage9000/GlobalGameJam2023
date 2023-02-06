using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{

    public UnityEvent OnCharacterSpot;
    public UnityEvent OnCharacterLost;

    public string PlayerTag = "Player";
    public string TrunkTag = "Trunk";
    public int damage = 5;

    GameObject PlayerObject;

    private void Awake() {
        PlayerObject = null;
    }

    private void Update() {
        if(PlayerObject) {
            transform.LookAt(PlayerObject.transform.position, Vector3.up);
            HealthSystem playerHP = PlayerObject.GetComponent<HealthSystem>();
            playerHP.TakeDamage(damage);
            Debug.Log("ATTACKING");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals(PlayerTag) || other.gameObject.tag.Equals(TrunkTag)) {
            Debug.Log(gameObject.tag);
            PlayerObject = other.gameObject;
            OnCharacterSpot.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals(PlayerTag) || other.gameObject.tag.Equals(TrunkTag)) {
            PlayerObject = null;
            OnCharacterLost.Invoke();
        }
    }
}
