using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Movement movement;
    [SerializeField] private SpriteRenderer sr;

    [Header("Animations")]
    [SerializeField] private Sprite[] idleFrames;
    [SerializeField] private Sprite[] runFrames;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite fallSprite;

    [Header("Settings")]
    [SerializeField] private float animationSpeed = 10f;

    private int frameIndex;
    private float timer;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (movement == null) movement = GetComponent<Movement>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (rb == null || sr == null || movement == null)
            return;

        float xVel = rb.linearVelocity.x;
        float yVel = rb.linearVelocity.y;

        bool grounded = IsGrounded();

        HandleFlip(xVel);

        if (!grounded)
        {
            if (yVel > 0)
                sr.sprite = jumpSprite;
            else
                sr.sprite = fallSprite;

            return;
        }

        if (Mathf.Abs(xVel) > 0.1f)
        {
            PlayAnimation(runFrames);
        }
        else
        {
            PlayAnimation(idleFrames);
        }
    }

    private void PlayAnimation(Sprite[] frames)
    {
        if (frames == null || frames.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f / animationSpeed)
        {
            timer = 0f;
            frameIndex++;

            if (frameIndex >= frames.Length)
                frameIndex = 0;

            sr.sprite = frames[frameIndex];
        }
    }

    private void HandleFlip(float x)
    {
        if (x > 0.1f)
            sr.flipX = false;
        else if (x < -0.1f)
            sr.flipX = true;
    }

    private bool IsGrounded()
    {
        if (movement.groundCheck == null)
            return false;

        return Physics2D.OverlapCircle(
            movement.groundCheck.position,
            movement.groundRadius,
            movement.groundLayer
        );
    }
}