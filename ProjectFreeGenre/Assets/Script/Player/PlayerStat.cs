using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamage, IDashable
{
    public PlayerUI playerUI;

    public event Action OnTakeDamageEvent;

    PlayerCondition health { get { return playerUI.health; } }
    PlayerCondition stamina { get { return playerUI.stamina; } }

    public float knockBackResistance; // Knockback ÀúÇ× ½ºÅÈ

    void Update()
    {
        if (health.curValue == 0f)
        {
            Die();
        }
        health.Add(health.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
    }

    public void Sprint(float DashPower)
    {
        stamina.Subtract(DashPower);
    }

    public void Die()
    {
        GameManager.Instance.player.knockback.StopRoutine();
        GameManager.Instance.GameOver();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Attacked(float damage)
    {
        health.Subtract(damage);
        OnTakeDamageEvent?.Invoke();
    }
}
