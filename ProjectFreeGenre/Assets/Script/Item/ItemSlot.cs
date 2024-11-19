using System;
using UnityEditor;
using UnityEngine;

public class ItemSlot
{
    private Player player;
    private PlayerEquipment equipment;
    public int index;
    public ItemData data { get; private set; }
    private EquipItem equipItem;
    public float CooltimeRemain;
    public bool isActivated { get; private set; }

    public event Action<float> OnCooltiomeProgressEvent;
    public event Action<bool> OnActivationChangedEvent;
    public event Action<int> OnDropEvent;

    public ItemSlot(Player player, int index, ItemData data)
    {
        this.player = player;
        this.equipment = player.equipment;
        this.index = index;
        this.data = data;
        
        equipment.TimeProgressCheckEvent += CheckCooltime;
        equipment.EquipmentRemoveEvent += ReduceIndex;

        GameObject WeaponObj = GameObject.Instantiate(ItemDataManager.Instance.Dict.dict[data.id].equipItem, player.transform);
        CooltimeRemain = data.attackRate;
        isActivated = true;

        equipItem = WeaponObj.GetComponent<EquipItem>();

        UIManager.Instance.itemSlotListUI.AddUI(this);
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
        
        equipItem.Unequiped();
        GameObject dropItem = ItemDataManager.Instance.GetDict_Drop(data.id);
        dropItem.transform.position = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
        OnDropEvent?.Invoke(index);
    }
}
