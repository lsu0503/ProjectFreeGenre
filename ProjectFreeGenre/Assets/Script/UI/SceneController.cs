using UnityEngine;

public class SceneController : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.sceneController = this;

        GameManager.Instance.OnGameClearEvent += DisplayGameClearUI;
        GameManager.Instance.OnGameOverEvent += DisplayGameOverUI;
        GameManager.Instance.OnSceneExitEvent += SceneExit;
    }

    private void SceneExit()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.ClearManager();
    }

    public void DisplayGameClearUI()
    {
        
    }

    public void DisplayGameOverUI()
    {
        
    }
}