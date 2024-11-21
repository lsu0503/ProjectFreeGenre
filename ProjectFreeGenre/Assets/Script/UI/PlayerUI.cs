using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerCondition health;
    public PlayerCondition stamina;

    private void Start()
    {
        GameManager.Instance.player.playerStat.playerUI = this;
    }
}
