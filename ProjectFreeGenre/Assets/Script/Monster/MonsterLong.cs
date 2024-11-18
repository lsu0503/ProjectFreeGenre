using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLong : Monster
{
    public GameObject bullet;
    private float attackDelayTmp;

    private void Start()
    {
        base.Start();
        attackDelayTmp = statSO.attackDelay;
    }
    protected override void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > statSO.distance)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * statSO.speed * Time.deltaTime;
        }
        else
        {
            AttackDelay();
        }
    }

    private void AttackDelay()
    {
        if(attackDelayTmp > 0)
        {
            attackDelayTmp -= Time.deltaTime;
        }
        else
        {
            attackDelayTmp = statSO.attackDelay;
            Attack();
        }
    }

    void Attack()
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
        MonsterBullet monsterBullet = bulletInstance.GetComponent<MonsterBullet>();

        monsterBullet.attack = statSO.attackBullet;
        monsterBullet.direction = (player.transform.position - transform.position).normalized;
    }
}
