using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Boss,
    Normal,
}
public enum AttackType
{
    ShortRange,
    LongRange,
}

[CreateAssetMenu(fileName = "MonsterStat", menuName = "ScriptableObject/Monster")]
public class MonsterStatSO : ScriptableObject
{
    public MonsterType monsterType;
    public AttackType attackType;

    public float hp;//체력
    public float attack;//공격력
    public float attackDelay;//공격딜레이
    public float speed;//이동속도
    public float knockBackResistance;//넉백저항력
    public float distance;//공격사거리
}
