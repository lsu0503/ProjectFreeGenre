using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRange : Monster
{
    public GameObject bullet;
    private float attackDelayTmp;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject firePoint;

    private void Start()
    {
        base.Start();
        attackDelayTmp = statSO.attackDelay;
    }
    protected override void Move(Vector3 direction)
    {
        if (isOnKnockback) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);


        if (distance > statSO.distance)
        {
            animator.SetBool("IsRun", true);
            Vector3 targetVelocity = direction * statSO.speed;
            rb.velocity = targetVelocity;
        }
        else
        {
            animator.SetBool("IsRun", false);
            // 공격 범위 내에 있을 때 속도를 0으로 설정
            rb.velocity = Vector3.zero;
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
        if (clip != null)
        {
            GameObject soundObj = SoundManager.Instance.PlayClip(clip);
            soundObj.transform.position = transform.position;
        }
        GameObject bulletInstance = Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(Vector3.zero));
        MonsterBullet monsterBullet = bulletInstance.GetComponent<MonsterBullet>();
        monsterBullet.attack = attackBullet;

        Vector3 playerProjection = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
        Vector3 selfProjection = new Vector3(transform.position.x, 0.0f, transform.position.z);

        monsterBullet.direction = (playerProjection - selfProjection).normalized;
    }
}
