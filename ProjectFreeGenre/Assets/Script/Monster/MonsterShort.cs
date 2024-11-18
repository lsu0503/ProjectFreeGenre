using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShort : Monster
{
    protected override void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > statSO.distance)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * statSO.speed * Time.deltaTime;
        }
    }
}
