using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : MonoBehaviour
{
    private ItemData data;
    private List<GameObject> hitList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        // 캐릭터(플레이어 & 몬스터)의 피격 함수 받아서 작성.
    }

    public void SetData(ItemData data)
    {
        this.data = data;
    }

    public void ClearHitData()
    {
        hitList.Clear();
    }
}