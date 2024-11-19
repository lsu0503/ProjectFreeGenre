using System;
using UnityEngine;

public class ItemSlot
{
    // Player 클래스가 없는 상태로 작업해서, Player 클래스 관련 항목은 주석 처리 하였습니다.

    private Player player;
    public int index;
    private ItemData data;
    private GameObject WeaponObj;
    public float CooltimeRemain;
    public bool isActivated { get; private set; }

    public event Action<float> OnCooltiomeProgressEvent;
    public event Action<bool> OnActivationChangedEvent;

    public ItemSlot(Player player, int index, ItemData data)
    {
        this.player = player;
        this.index = index;
        this.data = data;
        WeaponObj = GameObject.Instantiate(ItemDataManager.Instance.Dict.dict[data.id].equipItem, player.transform);
        WeaponObj.SetActive(false);
        CooltimeRemain = data.attackRate;
        isActivated = true;
    }

    public bool CheckCooltime(float time)
    {
        if (CooltimeRemain <= 0.0f)
            return true;

        CooltimeRemain -= time;
        OnCooltiomeProgressEvent?.Invoke(CooltimeRemain / data.attackRate);


        if (CooltimeRemain <= 0.0f)
            return true;

        else
            return false;
    }

    public void UseItem()
    {
        CooltimeRemain = data.attackRate;
        WeaponObj.SetActive(true);
    }

    public void ActivateSkill()
    {
        isActivated = true;
        OnActivationChangedEvent?.Invoke(true);
    }

    public void DeactivateSkill()
    {
        isActivated = false;
        OnActivationChangedEvent?.Invoke(false);
    }
}
