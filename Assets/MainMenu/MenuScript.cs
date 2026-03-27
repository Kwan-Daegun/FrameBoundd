using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool useAsyncLoading = true;
    [SerializeField] private float minLoadDelay = 0.25f;

    private bool isLoading = false;

    public void StartGame(string sceneName)
    {
        if (isLoading)
        {
            Debug.LogWarning("[Menu] Load already in progress.");
            return;
        }

        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("[Menu] Scene name is null or empty.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"[Menu] Scene '{sceneName}' is not in Build Settings.");
            return;
        }

        isLoading = true;

        Debug.Log("[Menu] Setting scene: " + sceneName);

        SceneLoader.TargetSceneName = sceneName;

        SceneManager.LoadScene("LoadingScene");
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        isLoading = true;

        float startTime = Time.time;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log($"[Menu] Loading progress: {progress:P0}");

            if (operation.progress >= 0.9f && Time.time - startTime >= minLoadDelay)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    public void BackToHome(string homeSceneName = "MainMenu")
    {
        if (isLoading)
        {
            Debug.LogWarning("[Menu] Load already in progress.");
            return;
        }

        if (string.IsNullOrWhiteSpace(homeSceneName))
        {
            Debug.LogError("[Menu] Home scene name is null or empty.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(homeSceneName))
        {
            Debug.LogError($"[Menu] Scene '{homeSceneName}' is not in Build Settings.");
            return;
        }

        isLoading = true;

        Debug.Log("[Menu] Returning to Home: " + homeSceneName);

        
        SceneLoader.TargetSceneName = homeSceneName;

        SceneManager.LoadScene("LoadingScene");
    }
    public void QuitGame()
    {
        Debug.Log("[Menu] Quit requested.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}