using System;
using UnityEditor;
using UnityEngine;

public class ItemSlot
{
    private Player player;
    private PlayerEquipment equipment;
    public int index;
    public ItemData data { get; private set; }
    private GameObject WeaponObj;
    private EquipItem equipItem;
    public float CooltimeRemain;
    public bool isActivated { get; private set; }

    public event Action<float> OnCooltiomeProgressEvent;
    public event Action<bool> OnActivationChangedEvent;

    public ItemSlot(Player player, int index, ItemData data)
    {
        this.player = player;
        this.equipment = player.equipment;
        this.index = index;
        this.data = data;
        
        equipment.TimeProgressCheckEvent += CheckCooltime;
        equipment.EquipmentRemoveEvent += ReduceIndex;

        WeaponObj = GameObject.Instantiate(ItemDataManager.Instance.Dict.dict[data.id].equipItem, player.transform);
        CooltimeRemain = data.attackRate;
        isActivated = true;

        equipItem = WeaponObj.GetComponent<EquipItem>();
    }

    private void ReduceIndex(int targetIndex)
    {
        if (targetIndex > index) return;

        index--;
    }

    public void CheckCooltime(float time)
    {
        CooltimeRemain -= time;
        OnCooltiomeProgressEvent?.Invoke(CooltimeRemain / data.attackRate);

        if (CooltimeRemain <= 0.0f && isActivated)
            UseItem();
    }

    public void UseItem()
    {
        CooltimeRemain = data.attackRate;
        equipItem.UseItem();
    }

    public void ChangeActivation(bool isActivated)
    {
        this.isActivated = isActivated;
        OnActivationChangedEvent?.Invoke(isActivated);
    }

    public void DropItem()
    {
        equipment.EquipmentRemoveEvent -= ReduceIndex;
        equipment.TimeProgressCheckEvent -= CheckCooltime;

        WeaponObj.GetComponent<EquipItem>().Unequiped();
        GameObject dropItem = ItemDataManager.Instance.GetDict_Drop(data.id);
        dropItem.transform.position = player.transform.position;
    }
}
