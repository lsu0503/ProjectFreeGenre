using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    public int id { get; }
    public ItemData data;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = data.sprite;
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