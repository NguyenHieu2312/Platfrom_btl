using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public static GameOverPanel Instance { get; private set; }
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ShowPanel() { if (gameOverPanel != null) gameOverPanel.SetActive(true); }
    public void HidePanel() { if (gameOverPanel != null) gameOverPanel.SetActive(false); }

    public void OnRestartButtonClicked()
    {
        GameManager.Instance.LoadGameScene();
    }

    public void OnBackToMenuButtonClicked()
    {
        GameManager.Instance.LoadMenuScene();
    }
}