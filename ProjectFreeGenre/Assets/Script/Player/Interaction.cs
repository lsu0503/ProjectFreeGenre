using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private IInteractable curInteractable;
    [SerializeField] private LayerMask targetLayers;
    private bool isNotCheckAfterInteraction;

    private void Start()
    {
        isNotCheckAfterInteraction = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckAround(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isNotCheckAfterInteraction)
        {
            CheckAround(other);
            isNotCheckAfterInteraction = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (curInteractable == null) return;

        if((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            IInteractable targetIntaractable = other.gameObject.GetComponent<IInteractable>();

            if (targetIntaractable == null) return;
            if (!ReferenceEquals(curInteractable, targetIntaractable)) return;

            curInteractable.UnsetInterActionTarget();
            curInteractable = null;

            isNotCheckAfterInteraction = true;
        }
    }

    private void CheckAround(Collider other)
    {
        if ((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            IInteractable targetIntaractable = other.gameObject.GetComponent<IInteractable>();

            if (targetIntaractable == null) return;
            if (ReferenceEquals(curInteractable, targetIntaractable)) return;
            if (curInteractable != null)
            {
                curInteractable.UnsetInterActionTarget();
            }

            targetIntaractable.SetInterActionTarget();
            curInteractable = targetIntaractable;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if (curInteractable != null)
            {
                curInteractable.OnInteraction();
                curInteractable = null;

                isNotCheckAfterInteraction = true;
            }
        }
    }
}