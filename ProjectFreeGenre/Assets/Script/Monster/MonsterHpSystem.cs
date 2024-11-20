using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour, IDamage
{
    public Monster monster;
    public Slider hpBar;
    public float hpTmp;

    void OnEnable()
    {
        hpTmp = monster.hp;
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
        hpBar.value = hpTmp / monster.hp;
        if (hpBar.value < 1 && hpBar.value > 0)
            hpBar.gameObject.SetActive(true);
        else
            hpBar.gameObject.SetActive(false);
    }
}
