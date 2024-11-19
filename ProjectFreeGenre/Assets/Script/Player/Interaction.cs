using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    private IInteractable curInteractable;
    [SerializeField] private LayerMask targetLayers;

    private void OnTriggerEnter(Collider other)
    {
        if((targetLayers & (1 << other.gameObject.layer)) != 0)
        {
            IInteractable targetIntaractable = other.gameObject.GetComponent<IInteractable>();

            if (targetIntaractable == null) return;
            if (ReferenceEquals(curInteractable, targetIntaractable)) return;

            curInteractable = targetIntaractable;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            curInteractable.OnInteraction();
        }
    }
}