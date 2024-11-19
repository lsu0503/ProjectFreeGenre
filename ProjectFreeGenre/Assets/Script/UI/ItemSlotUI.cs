using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    private int index;
    private ItemSlot slot;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image cooltimeGauge;

    private UIClicker clicker;

    // UI 상호작용 정보 초기화
    private void Awake()
    {
        clicker = GetComponent<UIClicker>();
    }

    private void Start()
    {
        clicker.OnLeftCilickEvent += DropSlot;
        clicker.OnRightCilickEvent += ChangeActivation;
    }

    // 아이템 정보 초기화
    public void SetUI(ItemSlot slot)
    {
        this.slot = slot;
        index = slot.index;
        slot.OnCooltiomeProgressEvent += CheckCooltime;
        slot.OnActivationChangedEvent += SetActivation;

        itemIcon.sprite = slot.data.sprite;
        itemIcon.SetNativeSize();
    }

    /* UI 효과 관련 메소드 */
    private void DropSlot()
    {
        slot.DropItem();
    }

    private void ChangeActivation()
    {
        slot.ChangeActivation(!slot.isActivated);
    }

    /* UI 표시 관련 메소드 */
    private void CheckCooltime(float ratio)
    {
        cooltimeGauge.fillAmount = ratio;
    }

    private void SetActivation(bool isActivated)
    {
        if (isActivated)
        {
            backgroundImg.color = Color.white;
        }

        else
        {
            backgroundImg.color = Color.gray;
        }
    }
}
