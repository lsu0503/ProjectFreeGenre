public class GameClearUI : InGameMenuUI
{
    protected override void Start()
    {
        GameManager.Instance.sceneController.gameClearUI = this;
        base.Start();
    }

    private void ActivateUI()
    {
        gameObject.SetActive(true);
    }
}