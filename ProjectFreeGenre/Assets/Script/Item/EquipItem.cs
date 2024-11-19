using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private ItemData data;
    
    private EquipRotater rotater;
    private ActionItem actionItem;

    private void Awake()
    {
        actionItem = GetComponentInChildren<ActionItem>();

        switch (data.targetType)
        {
            case TARGETTYPE.FLIP:
                rotater = gameObject.AddComponent<EquipFlip>();
                break;
            case TARGETTYPE.DIRECTION:
                rotater = gameObject.AddComponent<EquipDirection>();
                break;
            case TARGETTYPE.AIM:
                break;
        }
    }

    public void UseItem()
    {
        actionItem.StartUse();
    }

    public void Unequiped()
    {
        StartCoroutine(DestroyAfterDeactivated());
    }

    public IEnumerator DestroyAfterDeactivated()
    {
        yield return new WaitUntil(() => actionItem.gameObject.activeSelf == false);
        Destroy(gameObject);
    }
}
