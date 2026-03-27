using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 5f;
    public float maxFallSpeed = -40f;

    [Header("Jump Tuning")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private bool jumpRequested;
    private bool jumpHeld;

    private float moveX;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool wasGrounded;

    private Vector2 lastSafePosition;

    
    private float jumpStartY;
    private float highestY;
    private bool isJumping;

    [Header("UI")]
    public TextMeshProUGUI jumpHeightText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastSafePosition = transform.position;
        isGrounded = CheckGrounded();
        wasGrounded = isGrounded;
    }

    void Update()
    {
        isGrounded = CheckGrounded();

        if (isGrounded)
        {
            lastSafePosition = transform.position;
        }

        if (isJumping)
        {
            highestY = Mathf.Max(highestY, transform.position.y);

            if (jumpHeightText != null)
            {
                float currentJumpHeight = Mathf.Max(0f, highestY - jumpStartY);
                jumpHeightText.text = "Jump Height: " + currentJumpHeight.ToString("F2");
            }
        }

        // Finalize jump height only after a real landing transition.
        if (!wasGrounded && isGrounded && isJumping)
        {
            float jumpHeight = Mathf.Max(0f, highestY - jumpStartY);

            if (jumpHeightText != null)
            {
                jumpHeightText.text = "Jump Height: " + jumpHeight.ToString("F2");
            }

            isJumping = false;
        }

        wasGrounded = isGrounded;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * Speed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

                
                jumpStartY = transform.position.y;
                highestY = jumpStartY;
                isJumping = true;
            }
            jumpRequested = false;
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !jumpHeld)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveX = context.ReadValue<Vector2>().x;
        }
        else if (context.canceled)
        {
            moveX = 0f;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpRequested = true;
            jumpHeld = true;
        }

        if (context.canceled)
        {
            jumpHeld = false;
        }
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = lastSafePosition + Vector2.up * 0.1f;
        isJumping = false;
        jumpRequested = false;
        jumpHeld = false;
        jumpStartY = transform.position.y;
        highestY = jumpStartY;
        isGrounded = CheckGrounded();
        wasGrounded = isGrounded;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private bool CheckGrounded()
    {
        if (groundCheck == null)
        {
            return false;
        }

        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }
}
