using System;
using UnityEngine;

[Serializable]
public class ItemDictionaryContent : IDictionaryContent
{
    public ItemData data;
    public GameObject dropItem;
    public GameObject equipItem;

    public int ID { get { return data.id; } }
}