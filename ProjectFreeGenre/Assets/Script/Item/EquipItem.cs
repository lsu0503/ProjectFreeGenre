using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private ItemData data;
    private List<GameObject> hitList = new List<GameObject>();
    private Animator animator;
    private Collider attackCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = data.animator;
        attackCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 캐릭터(플레이어 & 몬스터)의 피격 함수 받아서 작성.
    }

    public void OnUse()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Activate");

    }

    private IEnumerator AttackTimeCheck()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        EndUse();
    }

    private void EndUse()
    {
        gameObject.SetActive(false);
    }

    public void AttackOn()
    {
        attackCollider.enabled = true;
    }

    public void AttackOff()
    {
        attackCollider.enabled = false;
    }
}