using UnityEngine;

public class WinGamePanel : MonoBehaviour
{
    public static WinGamePanel Instance { get; private set; }
    [SerializeField] private GameObject winGamePanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (winGamePanel != null) winGamePanel.SetActive(false);
    }

    public void ShowPanel() { if (winGamePanel != null) winGamePanel.SetActive(true); }
    public void HidePanel() { if (winGamePanel != null) winGamePanel.SetActive(false); }

    public void OnRestartButtonClicked()
    {
        GameManager.Instance.LoadGameScene();
    }

    public void OnBackToMenuButtonClicked()
    {
        GameManager.Instance.LoadMenuScene();
    }
}