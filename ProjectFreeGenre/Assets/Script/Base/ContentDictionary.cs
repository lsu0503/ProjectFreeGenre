using System.Collections.Generic;
using UnityEngine;

public interface IDictionaryContent
{
    public int ID { get; }
}

public class ContentDictionary<T> where T : IDictionaryContent
{
    public Dictionary<int, T> dict;

    public T GetDict(int key)
    {
        return dict[key];
    }

    public bool AddDict(T content)
    {
        if (dict.ContainsKey(content.ID)) return false;

        else
        {
            dict.Add(content.ID, content);
            return true;
        }
    }

    public void ClearDict()
    {
        dict.Clear();
    }
}