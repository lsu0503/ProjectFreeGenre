using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image hpBar;
    public ItemSlotListUI itemSlotListUI;

    public void UpdateHpBar(float currentHp, float maxHp) 
    {
        hpBar.fillAmount = currentHp / maxHp; 
    }
}