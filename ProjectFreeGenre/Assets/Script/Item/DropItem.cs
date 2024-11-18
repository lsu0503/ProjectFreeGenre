using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    public int id { get; }
    public ItemData data { get; private set; }
    private Image image;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        image.sprite = data.sprite;
    }

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