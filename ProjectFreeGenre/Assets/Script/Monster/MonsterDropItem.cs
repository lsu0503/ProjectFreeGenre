using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDropItem : MonoBehaviour
{
    public GameObject[] items;
    public MonsterHpSystem hpSystem;

    private void Start()
    {
        hpSystem.OnDie += DropItem;
    }

    private void DropItem()
    {
        int randomIndex = Random.Range(0, 3);
        Instantiate(items[randomIndex], new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), Quaternion.identity);
    }
}
