using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string menuSceneName = "MenuScene";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == gameSceneName)
            PauseGame();    
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
        Time.timeScale = 1f;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
        Time.timeScale = 1f; 
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        PauseGamePanel.Instance.ShowPanel();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseGamePanel.Instance.HidePanel(); 
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverPanel.Instance.ShowPanel();
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        WinGamePanel.Instance.ShowPanel();
    }
}