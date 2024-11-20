using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour, IDamage
{
    private Monster monster;
    public Slider hpBar;
    public float hpTmp;

    private void Awake()
    {
        hpBar = GetComponentInChildren<Slider>();
        monster = GetComponent<Monster>();
    }

    void OnEnable()
    {
        hpTmp = monster.statSO.hp;
    }

    public void Attacked(float damage)
    {
        hpTmp -= damage;
        HpUpdate();
        if (hpTmp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("적 사망");
        GameManager.Instance.monsters.Remove(gameObject);
        gameObject.SetActive(false);
        // 여기에 사망 처리 로직 추가
    }

    public void HpUpdate()
    {
        hpBar.value = hpTmp / monster.statSO.hp;
        if (hpBar.value < 1 && hpBar.value > 0)
            hpBar.gameObject.SetActive(true);
        else
            hpBar.gameObject.SetActive(false);
    }
}
