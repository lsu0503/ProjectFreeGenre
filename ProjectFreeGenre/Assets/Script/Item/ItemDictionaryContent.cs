using System;
using UnityEngine;

[Serializable]
public class ItemDictionaryContent : IDictionaryContent
{
    public int id;
    public ItemData data;
    public GameObject dropItem;
    public GameObject equipItem;

    public int ID { get { return id; } }
}