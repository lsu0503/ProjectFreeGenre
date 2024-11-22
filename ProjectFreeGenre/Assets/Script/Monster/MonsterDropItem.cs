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

        GameObject dropItem = ItemDataManager.Instance.GetDict_Drop(randomIndex);
        dropItem.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }
}
