using System.Collections.Generic;
using UnityEngine;

public class ItemDictionaryConstructer : MonoBehaviour
{
    [SerializeField] private List<ItemDictionaryContent> contentList;

    private void Awake()
    {
        if (contentList.Count <= 0) return;

        ContentDictionary<ItemDictionaryContent> tempDict = new ContentDictionary<ItemDictionaryContent>();

        foreach(ItemDictionaryContent content in contentList)
        {
            tempDict.AddDict(content);
        }

        ItemDataManager.Instance.SetDict(tempDict);

        Destroy(this);
    }
}
