using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    public float damage_resistance = 1.3f;

    private void Awake() {
        OnDeath.AddListener(Die);
    }


    public void Die() {
        Debug.Log("Died");
        GameObject.Destroy(gameObject);
    }
/*    public override void TakeDamage(uint damage) {
        float new_damage = damage / damage_resistance;
        uint rounded_damage = (uint)Mathf.CeilToInt(new_damage);
        base.TakeDamage(rounded_damage);
    }*/

    private void OnTriggerEnter(Collider other) {
        /*        if(other.gameObject.tag.Equals("Enemy")) {
                    TakeDamage(20);
                }*/
        Debug.Log("Hit");
        if (other.gameObject.tag.Equals("Player")) {
            TakeDamage(5);
        }
    }

    private void OnTriggerStay(Collider other) {
/*        if (other.gameObject.tag.Equals("Enemy")) {
            TakeDamage(20);
        }*/
        if (other.gameObject.tag.Equals("Player")) {
            TakeDamage(5);
        }
    }
}
