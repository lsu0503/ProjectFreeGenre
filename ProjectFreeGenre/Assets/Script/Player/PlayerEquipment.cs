using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private List<ItemSlot> slots = new List<ItemSlot>();
    [SerializeField] private int maxSlot;

    public event Action<float> TimeProgressCheckEvent;
    public event Action<int> EquipmentRemoveEvent;

    private void FixedUpdate()
    {
        TimeProgressCheckEvent?.Invoke(Time.deltaTime);
    }

    public bool AddItem(ItemData data)
    {
        if (slots.Count >= maxSlot)
        {
            return false;
        }

        ItemSlot slot = new ItemSlot(GameManager.Instance.player, slots.Count, data);
        slots.Add(slot);
        return true;
    }

    public void RemoveItem(int index)
    {
        slots.RemoveAt(index);
        EquipmentRemoveEvent?.Invoke(index);
    }
}