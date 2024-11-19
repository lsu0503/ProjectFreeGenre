using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image hpBar;
    public void UpdateHpBar(int currentHp, int maxHp) 
    {
        hpBar.fillAmount = (float)currentHp / maxHp; 
    }
}