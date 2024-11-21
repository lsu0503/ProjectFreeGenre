using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClicker : MonoBehaviour, IPointerClickHandler
{
    public event Action OnLeftCilickEvent;
    public event Action OnMiddleClickEvent;
    public event Action OnRightCilickEvent;

    public void OnPointerClick(PointerEventData eventData)
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