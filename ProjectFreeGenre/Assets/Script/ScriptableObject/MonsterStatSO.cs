using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Normal,
    Range,
    Boss
}

[CreateAssetMenu(fileName = "MonsterStat", menuName = "New Monster Stat")]
public class MonsterStatSO : ScriptableObject
{
    public StatType type;

    public float hp;//체력
    public float attack;//공격력
    public float attackDelay;//공격딜레이
    public float speed;//이동속도
    public float knockBackResistance;//넉백저항력
    public float distance;//공격사거리
}
