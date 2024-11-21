using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenuUI : MonoBehaviour
{
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void RetryButton()
    {
        GameManager.Instance.ClearManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TitleButton()
    {
        GameManager.Instance.ClearManager();
        SceneManager.LoadScene("TitleScene");
    }
}
