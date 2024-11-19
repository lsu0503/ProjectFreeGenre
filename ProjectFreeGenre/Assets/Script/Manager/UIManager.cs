using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image hpBar;
    public ItemSlotListUI itemSlotListUI;

    public void UpdateHpBar(int currentHp, int maxHp) 
    {
        hpBar.fillAmount = (float)currentHp / maxHp; 
    }
}