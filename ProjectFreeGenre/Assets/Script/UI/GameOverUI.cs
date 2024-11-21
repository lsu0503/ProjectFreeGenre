using TMPro;

public class GameOverUI : InGameMenuUI
{
    public TextMeshProUGUI curTimeText;

    protected override void Start()
    {
        GameManager.Instance.sceneController.gameOverUI = this;
        base.Start();
    }

    private void ActivateUI()
    {
        gameObject.SetActive(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        curTimeText.text = string.Format($"{(int)(GameManager.Instance.currentTIme / 60), 2}:{(GameManager.Instance.currentTIme % 60),-6:N3}");
    }
}
