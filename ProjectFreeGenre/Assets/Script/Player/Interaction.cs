using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private IInteractable curInteractable;
    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            IInteractable targetIntaractable = other.gameObject.GetComponent<IInteractable>();

            if (targetIntaractable == null) return;
            if (ReferenceEquals(curInteractable, targetIntaractable)) return;
            if(curInteractable != null)
            {
                curInteractable.UnsetInterActionTarget();
            }

            targetIntaractable.SetInterActionTarget();
            curInteractable = targetIntaractable;
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
            }
        }
    }
}