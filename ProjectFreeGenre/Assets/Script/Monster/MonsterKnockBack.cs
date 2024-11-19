using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnockBack : MonoBehaviour, IKnockback
{
    private Monster monster;

    void Awake()
    {
        monster = GetComponent<Monster>();
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        float adjustedForce = force / monster.statSO.knockBackResistance;

        // Rigidbody에 힘을 가하여 넉백 적용
        monster.rb.AddForce(direction.normalized * adjustedForce, ForceMode.Impulse);
    }
}