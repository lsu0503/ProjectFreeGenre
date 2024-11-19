using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour
{
    private Monster monster;
    public Slider hpBar;

    private void Awake()
    {
        hpBar = GetComponentInChildren<Slider>();
        monster = GetComponent<Monster>();
    }

    public void HpUpdate()
    {
        hpBar.value = monster.hpTmp / monster.statSO.hp;
        if (hpBar.value < 1)
            hpBar.gameObject.SetActive(true);
        else
            hpBar.gameObject.SetActive(false);
    }
}
