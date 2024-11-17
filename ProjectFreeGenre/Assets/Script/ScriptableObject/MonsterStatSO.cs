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

    public float hp;//ü��
    public float attack;//���ݷ�
    public float attackDelay;//���ݵ�����
    public float speed;//�̵��ӵ�
    public float knockBackResistance;//�˹����׷�
    public float distance;//���ݻ�Ÿ�
}
