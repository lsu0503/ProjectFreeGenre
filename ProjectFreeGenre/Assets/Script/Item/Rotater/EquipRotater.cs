using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class EquipRotater : MonoBehaviour
{
    protected ActionItem actionItem;
    protected Coroutine routine;

    protected virtual void Awake()
    {
        actionItem = GetComponentInChildren<ActionItem>();
    }

    protected abstract void ChangeDirection();

    public IEnumerator ChangeDiredtionAfterDeactivated()
    {
        yield return new WaitUntil(() => actionItem.gameObject.activeSelf == false);
        ChangeDirection();
    }

    public virtual void RemoveAction()
    {
        StopCoroutine(routine);
    }
}
