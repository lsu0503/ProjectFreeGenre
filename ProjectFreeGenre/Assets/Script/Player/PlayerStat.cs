using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamage
{
    public PlayerUI playerUI;

    public event Action onTakeDamage;

    PlayerCondition health { get { return playerUI.health; } }
    public float CurrentHp { get; set; }
    public int Speed { get; set; }
    public int AttackPower { get; set; }
    public int AttackDelay { get; set; }

    void Update()
    {
        if (health.curValue == 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("ав╬З╢ы!");
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Attacked(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

}
