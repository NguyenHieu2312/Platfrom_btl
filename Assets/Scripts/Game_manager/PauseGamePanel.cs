using UnityEngine;

public class PauseGamePanel : MonoBehaviour
{
    public static PauseGamePanel Instance { get; private set; }

    [SerializeField] private GameObject pausePanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("PausePanel is not assigned in Inspector!");
        }
    }

    public void ShowPanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    public void HidePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void OnResumeButtonClicked()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnBackToMenuButtonClicked()
    {
        GameManager.Instance.LoadMenuScene();
    }
}