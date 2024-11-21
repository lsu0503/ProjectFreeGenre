using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
            gameObject.SetActive(true);
        }

        else
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }

    public void RetryButton()
    {
        GameManager.Instance.CallOnSceneExit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TitleButton()
    {
        GameManager.Instance.CallOnSceneExit();
        SceneManager.LoadScene("TitleScene");
    }
}
