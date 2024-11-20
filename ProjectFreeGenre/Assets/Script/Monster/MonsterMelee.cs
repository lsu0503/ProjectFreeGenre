using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMelee : Monster
{
    protected override void Move(Vector3 direction)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > statSO.distance)
        {
            animator.SetBool("IsRun", true);
            Vector3 targetVelocity = direction * statSO.speed;
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime);
        }
    }
}
