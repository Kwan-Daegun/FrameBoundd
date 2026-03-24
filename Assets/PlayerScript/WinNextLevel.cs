using UnityEngine;
using UnityEngine.SceneManagement;
public class WinNextLevel : MonoBehaviour
{
    public string nextLevelName;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
