using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotListUI : MonoBehaviour
{
    [SerializeField] private GameObject ItemSlotUIPrefab;
    private List<ItemSlotUI> UIList = new List<ItemSlotUI>();

    private void Awake()
    {
        UIManager.Instance.itemSlotListUI = this;
    }

    public void AddUI(ItemSlot slot)
    {
        slot.OnDropEvent += RemoveItemSlotUI;

        GameObject tempObj = Instantiate(ItemSlotUIPrefab, this.transform);
        ItemSlotUI tempComp = tempObj.GetComponent<ItemSlotUI>();
        UIList.Add(tempComp);

        if (tempComp != null)
            tempComp.SetUI(slot, this);
    }

    private void RemoveItemSlotUI(int index)
    {
        UIList.RemoveAt(index);
    }
}