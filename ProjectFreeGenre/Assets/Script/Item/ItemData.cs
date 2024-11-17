using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public int id;
    public int damage;
    public int attackRate;
    public Sprite icon;
    public AnimatorOverrideController animator;
}
