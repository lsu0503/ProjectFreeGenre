using UnityEngine.InputSystem;

public class PauseMenuUI : InGameMenuUI
{
    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}