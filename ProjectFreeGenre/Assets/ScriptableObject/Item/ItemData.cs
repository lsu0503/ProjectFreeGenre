using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TARGETTYPE
{
    FLIP,        // 캐릭터의 좌우에 따라서 공격 방향 변경
    DIRECTION,   // 캐릭터의 이동 방향에 따라서 공격 방향 변경
    AIM          // 캐릭터에 가장 가까운 대상 공격
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public int damage;
    public int knockbackPower;
    public int attackRate;
    public Sprite sprite;
    public AnimatorOverrideController animator;
    public TARGETTYPE targetType;
}
