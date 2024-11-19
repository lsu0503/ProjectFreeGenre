using UnityEngine;

public class ItemDataManager : Singleton<ItemDataManager>
{
    private ContentDictionary<ItemDictionaryContent> dict;

    public ContentDictionary<ItemDictionaryContent> Dict { get { return dict; } }

    public void SetDict(ContentDictionary<ItemDictionaryContent> dict)
    {
        this.dict = dict;
    }

    public ItemData GetDict(int key)
    {
        return dict.GetDict(key).data;
    }

    public GameObject GetDict_Drop(int key)
    {
        return Instantiate(dict.GetDict(key).dropItem);
    }

    public GameObject GetDict_Equip(int key)
    {
        return Instantiate(dict.GetDict(key).dropItem);
    }
}