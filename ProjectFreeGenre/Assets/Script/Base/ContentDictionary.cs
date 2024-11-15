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

    public void AddDict(T content)
    {
        dict.Add(content.ID, content);
    }

    public void ClearDict()
    {
        dict.Clear();
    }
}