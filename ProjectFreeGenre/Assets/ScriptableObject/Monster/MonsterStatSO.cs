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

    public float hp;//ü��
    public float attack;//���ݷ�
    public float attackDelay;//���ݵ�����
    public float speed;//�̵��ӵ�
    public float knockBackResistance;//�˹����׷�
    public float distance;//���ݻ�Ÿ�
}
