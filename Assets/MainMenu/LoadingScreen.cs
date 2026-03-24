using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;

    private void Start()
    {
        if (string.IsNullOrEmpty(SceneLoader.TargetSceneName))
        {
            Debug.LogError("[LoadingScreen] TargetSceneName is null or empty.");
            return;
        }

        StartCoroutine(LoadAsync(SceneLoader.TargetSceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            if (loadingBar != null)
                loadingBar.value = progress;

            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.3f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}