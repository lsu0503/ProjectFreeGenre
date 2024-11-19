using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClicker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMouseOn;

    public event Action OnLeftCilickEvent;
    public event Action OnMiddleClickEvent;
    public event Action OnRightCilickEvent;

    private void Start()
    {
        isMouseOn = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOn = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMouseOn)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftCilickEvent?.Invoke();
                    break;

                case PointerEventData.InputButton.Middle:
                    OnMiddleClickEvent?.Invoke();
                    break;

                case PointerEventData.InputButton.Right:
                    OnRightCilickEvent?.Invoke();
                    break;
            }
        }
    }
}