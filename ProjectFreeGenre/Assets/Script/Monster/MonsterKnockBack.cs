using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnockBack : CharacterKnockBack
{
    public Vector3 direction;

    protected override void KnockBack(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            direction = (transform.position - collision.transform.position).normalized;

            rigidbody.velocity = Vector3.zero;
            //TODO :: ³Ë¹é ÀúÇ×·Â
            rigidbody.AddForce(direction * 30, ForceMode.Impulse);
        }
    }
}