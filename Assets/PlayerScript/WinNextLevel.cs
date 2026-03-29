using UnityEngine;
using UnityEngine.SceneManagement;

public class WinNextLevel : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (string.IsNullOrEmpty(nextLevelName))
        {
            Debug.LogError("Next level name is not set!", this);
            return;
        }

        
        SceneLoader.TargetSceneName = nextLevelName;

        
        SceneManager.LoadScene("LoadingScene");
    }
}