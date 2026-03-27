using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    [Header("Animation Frames")]
    public Sprite[] frames;

    [Header("Animation Settings")]
    public float frameRate = 10f; 
    public bool loop = true;
    public bool playOnStart = true;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    private bool isPlaying = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("ArrowAnimation requires a SpriteRenderer!");
        }
    }

    void Start()
    {
        if (playOnStart)
            Play();
    }

    void Update()
    {
        if (!isPlaying || frames == null || frames.Length == 0)
            return;

        timer += Time.deltaTime;

        float frameTime = 1f / frameRate;

        if (timer >= frameTime)
        {
            timer -= frameTime;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                if (loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame = frames.Length - 1;
                    isPlaying = false;
                }
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }

    
    public void Play()
    {
        if (frames == null || frames.Length == 0)
        {
            Debug.LogWarning("No frames assigned!");
            return;
        }

        isPlaying = true;
        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[currentFrame];
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Resume()
    {
        isPlaying = true;
    }
}