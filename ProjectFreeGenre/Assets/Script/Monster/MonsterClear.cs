using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClear : MonoBehaviour
{
    public MonsterHpSystem hpSystem;

    private void Start()
    {
        hpSystem.OnDieEvent += StageClear;
    }

    private void StageClear()
    {
        GameManager.Instance.GameClear();
    }
}
