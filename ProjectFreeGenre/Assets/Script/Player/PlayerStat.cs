using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamage
{
    public float MaxHp { get; set; }
    public float CurrentHp { get; set; }
    public int Speed { get; set; }
    public int AttackPower { get; set; }
    public int AttackDelay { get; set; }

    private void Awake()
    {
        MaxHp = 100;
        CurrentHp = MaxHp;
    }

    public void Attacked(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp < 0) CurrentHp = 0;
        Debug.Log("피격 데미지: " + CurrentHp);
        UIManager.Instance.UpdateHpBar(CurrentHp, MaxHp);
    }
}
