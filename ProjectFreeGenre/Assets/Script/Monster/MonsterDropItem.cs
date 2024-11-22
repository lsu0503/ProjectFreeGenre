using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDropItem : MonoBehaviour
{
    public MonsterHpSystem hpSystem;

    private void Start()
    {
        hpSystem.OnDieEvent += DropItem;
    }

    private void DropItem()
    {
        int randomIndex = Random.Range(0, 3);
        Vector3 dropPosition = transform.position;
        dropPosition.y = 0.0f;

        GameObject dropItem = ItemDataManager.Instance.GetDict_Drop(randomIndex);
        dropItem.transform.position = dropPosition;
    }
}
