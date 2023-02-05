using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    public float damage_resistance = 1.3f;

    public override void TakeDamage(uint damage) {
        float new_damage = damage / damage_resistance;
        uint rounded_damage = (uint)Mathf.CeilToInt(new_damage);
        base.TakeDamage(rounded_damage);
    }


}
