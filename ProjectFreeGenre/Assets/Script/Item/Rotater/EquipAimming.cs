using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EquipAimming : EquipRotater
{
    private bool isAttacking;
    private float DirectionCheckRate;
    private GameObject target;

    private void Start()
    {
        isAttacking = false;
        actionItem.OnUseEvent += ChangeTarget;
        actionItem.OnUseEvent += SetActivation;
        actionItem.OnEndEvent += UnsetActivation;
        target = null;
    }

    private void Update()
    {
        if (isAttacking)
        {
            ChangeDirection();
        }
    }

    protected override void ChangeDirection()
    {
        Vector3 resultVector = target.transform.position - transform.position;
        float angle = Mathf.Atan2(-resultVector.z, resultVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ChangeTarget()
    {
        float minSqrMagnitude = float.MaxValue;
        float curSqrMagnitude;

        Vector3 DistanceVector;

        List<GameObject> monsters = GameManager.Instance.monsters;

        if (monsters.Count <= 0)
            return;

        foreach (GameObject monster in monsters)
        {
            DistanceVector = monster.transform.position - transform.position;
            curSqrMagnitude = DistanceVector.sqrMagnitude;

            if (curSqrMagnitude < minSqrMagnitude)
            {
                minSqrMagnitude = curSqrMagnitude;
                target = monster;
            }
        }
    }

    public void SetActivation()
    {
        isAttacking = true;
    }

    public void UnsetActivation()
    {
        isAttacking = false;
    }

    public override void RemoveAction()
    {
        actionItem.OnUseEvent -= ChangeTarget;
        base.RemoveAction();
    }
}