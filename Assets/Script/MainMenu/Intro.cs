using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] logoVideos;
    public string nextSceneName = "MainMenu";

    private int currentIndex = 0;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        if (logoVideos == null || logoVideos.Length == 0)
        {
            Debug.LogError("No videos assigned!");
            LoadNextScene();
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
        PlayCurrentVideo();
    }

    void Update()
    {
        
        bool keyPressed = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;

        bool touchPressed = Touchscreen.current != null &&
                            Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if (keyPressed || touchPressed)
        {
            SkipCurrentVideo();
        }
    }

    void SkipCurrentVideo()
    {
        currentIndex++;

        if (currentIndex < logoVideos.Length)
        {
            PlayCurrentVideo();
        }
        else
        {
            LoadNextScene();
        }
    }

    void PlayCurrentVideo()
    {
        videoPlayer.Stop();
        videoPlayer.clip = logoVideos[currentIndex];
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        currentIndex++;

        if (currentIndex < logoVideos.Length)
        {
            PlayCurrentVideo();
        }
        else
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;

        
        SceneLoader.TargetSceneName = nextSceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}