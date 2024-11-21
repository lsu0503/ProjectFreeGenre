using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneController : MonoBehaviour
{
    public GameOverUI gameOverUI;
    public GameClearUI gameClearUI;

    private void Awake()
    {
        GameManager.Instance.sceneController = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameClearEvent += DisplayGameClearUI;
        GameManager.Instance.OnGameOverEvent += DisplayGameOverUI;
    }

    public void DisplayGameClearUI()
    {
        gameClearUI.gameObject.SetActive(true);
    }

    public void DisplayGameOverUI()
    {
        gameOverUI.gameObject.SetActive(true);
    }
}