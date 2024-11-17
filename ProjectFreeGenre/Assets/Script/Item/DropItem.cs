using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int id { get; }
    public ItemData data { get; private set; }

    public void OnInteraction()
    {
        GetItem();
        Destroy(gameObject);
    }

    public void GetItem()
    {
        //Player측의 변수(ItemSlot)을 받아서 작성.
    }
}