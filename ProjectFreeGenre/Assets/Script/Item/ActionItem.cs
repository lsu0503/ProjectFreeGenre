using UnityEngine;

public class ActionItem : MonoBehaviour
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
        attackComponent.ClearHitData();
        gameObject.SetActive(true);
    }

    private void EndUse()
    {
        gameObject.SetActive(false);
    }

    public void AttackOn()
    {
        attackObj.SetActive(true);
    }

    public void AttackOff()
    {
        attackObj.SetActive(false);
        attackComponent.ClearHitData();
    }
}