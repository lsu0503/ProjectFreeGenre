public class ItemDataManager : Singleton<ItemDataManager>
{
    // 최종적으로는 GameManager나 StageManager로 합치는 것이 좋아 보입니다.
    private ContentDictionary<ItemDictionaryContent> dict;

    public ContentDictionary<ItemDictionaryContent> Dict { get { return dict; } }

    public void SetDict(ContentDictionary<ItemDictionaryContent> dict)
    {
        this.dict = dict;
    }
}