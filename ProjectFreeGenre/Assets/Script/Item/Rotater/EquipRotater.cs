using System.Collections;
using UnityEngine;

public abstract class EquipRotater : MonoBehaviour
{
    protected Vector2 frontDir;
    private ActionItem actionItem;

    protected virtual void Awake()
    {
        actionItem = GetComponentInChildren<ActionItem>();
    }

    protected virtual void Start()
    {
        GameManager.Instance.player.playerMove.OnDirectionChanged += OnDirectionChange;
    }

    public void OnDirectionChange(Vector2 direction)
    {
        frontDir = direction;
        StartCoroutine(ChangeDiredtionAfterDeactivated());
    }

    protected abstract void ChangeDirection();

    public IEnumerator ChangeDiredtionAfterDeactivated()
    {
        yield return new WaitUntil(() => actionItem.gameObject.activeSelf == false);
        ChangeDirection();
    }
}