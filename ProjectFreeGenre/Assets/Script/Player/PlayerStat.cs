using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamage, IDashable
{
    public PlayerUI playerUI;

    public event Action onTakeDamage;

    PlayerCondition health { get { return playerUI.health; } }

    PlayerCondition stamina { get { return playerUI.stamina; } }

    public int Speed { get; set; }
    public int AttackPower { get; set; }
    public int AttackDelay { get; set; }

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime * 2);
        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Sprint(float DashPower)
    {
        stamina.Subtract(DashPower);
    }

    public void Die()
    {
        GameManager.Instance.GameOver();
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
