using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour, IDamage
{
    private Monster monster;
    public Slider hpBar;

    private void Awake()
    {
        hpBar = GetComponentInChildren<Slider>();
        monster = GetComponent<Monster>();
    }

    public void Attacked(float damage)
    {
        monster.hpTmp -= damage;
        HpUpdate();
        if (monster.hpTmp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("적 사망");
        // 여기에 사망 처리 로직 추가
    }

    public void HpUpdate()
    {
        hpBar.value = monster.hpTmp / monster.statSO.hp;
        if (hpBar.value < 1 && hpBar.value > 0)
            hpBar.gameObject.SetActive(true);
        else
            hpBar.gameObject.SetActive(false);
    }
}
