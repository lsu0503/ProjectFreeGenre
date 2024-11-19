using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private ItemData data;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject attackObj;
    private ItemAttack attackComponent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attackComponent = attackObj.GetComponent<ItemAttack>();

        switch (data.targetType)
        {
            case TARGETTYPE.FLIP:
                transform.parent.AddComponent<EquipFlip>();
                break;
            case TARGETTYPE.DIRECTION:
                transform.parent.AddComponent<EquipDirection>();
                break;
            case TARGETTYPE.AIM:
                break;
        }
    }

    private void Start()
    {
        animator.runtimeAnimatorController = data.animator;
        spriteRenderer.sprite = data.sprite;
        attackComponent.SetData(data);
        attackObj.SetActive(false);
    }

    private void EndUse()
    {
        attackComponent.ClearHitData();
        gameObject.SetActive(false);
    }

    public void AttackOn()
    {
        attackObj.SetActive(true);
    }

    public void AttackOff()
    {
        attackObj.SetActive(false);
    }
}
