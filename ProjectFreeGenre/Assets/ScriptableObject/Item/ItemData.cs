using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TARGETTYPE
{
    FLIP,        // ĳ������ �¿쿡 ���� ���� ���� ����
    DIRECTION,   // ĳ������ �̵� ���⿡ ���� ���� ���� ����
    AIM          // ĳ���Ϳ� ���� ����� ��� ����
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public int damage;
    public int knockbackPower;
    public float attackRate;
    public Sprite sprite;
    public AnimatorOverrideController animator;
    public TARGETTYPE targetType;
    public LayerMask targetLayers;
    public AudioClip clip;
}
