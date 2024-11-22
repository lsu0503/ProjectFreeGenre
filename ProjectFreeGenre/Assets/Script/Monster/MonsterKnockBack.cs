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

        // Rigidbody�� ���� ���Ͽ� �˹� ����
        monster.rb.AddForce(direction.normalized * adjustedForce, ForceMode.Impulse);

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