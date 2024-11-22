using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKnockBack : MonoBehaviour, IKnockback
{
    private Monster monster;
    private Coroutine routine;

    public void SetMonster(Monster monster)
    {
        this.monster = monster;
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        float adjustedForce = force / monster.statSO.knockBackResistance;

        direction.y = 0;
        direction = direction.normalized;
        // Rigidbody¿¡ ÈûÀ» °¡ÇÏ¿© ³Ë¹é Àû¿ë
        monster.rb.AddForce(direction * adjustedForce, ForceMode.Impulse);

        if(routine != null) 
            StopCoroutine(routine);

        if (this != null && this.gameObject != null && this.gameObject.activeInHierarchy)
            routine = StartCoroutine(KnockBackRecover());
    }

    public IEnumerator KnockBackRecover()
    {
        monster.isOnKnockback = true;
        yield return new WaitForSeconds(ConstantCollection.knockbackTime);
        monster.isOnKnockback = false;
    }

    public void StopRoutine()
    {
        StopAllCoroutines();
        monster.isOnKnockback = false;   
    }
}