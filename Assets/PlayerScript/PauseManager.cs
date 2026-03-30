using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneLoader.TargetSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LoadingScene");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.TargetSceneName = mainMenuSceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}