using System;
using UnityEngine;

public class ActionItem : MonoBehaviour
{
    [SerializeField] private ItemData data;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject attackObj;
    private ItemAttack attackComponent;
    public event Action OnUseEvent;
    public event Action OnEndEvent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attackComponent = attackObj.GetComponent<ItemAttack>();
    }

    private void Start()
    {
        animator.runtimeAnimatorController = data.animator;
        spriteRenderer.sprite = data.sprite;
        attackComponent.SetData(data);
        attackObj.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StartUse()
    {
        OnUseEvent?.Invoke();
        attackComponent.ClearHitData();
        gameObject.SetActive(true);
    }

    private void EndUse()
    {
        OnEndEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void AttackOn()
    {
        if (data.clip != null)
        {
            GameObject SoundObj = SoundManager.Instance.PlayClip(data.clip);
            SoundObj.transform.position = transform.position;
        }

        attackObj.SetActive(true);
    }

    public void AttackOff()
    {
        attackObj.SetActive(false);
        attackComponent.ClearHitData();
    }
}