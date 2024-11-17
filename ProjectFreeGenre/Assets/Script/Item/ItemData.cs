using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public int damage;
    public int attackRate;
    public Sprite icon;
    public AnimatorOverrideController animator;
}
