using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        GameManager.Instance.LoadGameScene();
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}