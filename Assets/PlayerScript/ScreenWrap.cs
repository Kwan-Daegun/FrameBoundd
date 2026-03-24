using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public LayerMask wallLayer;
    public float screenPadding = 1.0f;

    [Header("Stabilization")]
    public Transform screenAnchor;

    private Rigidbody2D rb;
    private Camera cam;
    private float width;
    private float height;
    private Collider2D col;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void LateUpdate()
    {
        height = cam.orthographicSize * 2f;
        width = height * cam.aspect;

        Vector3 centerPos = screenAnchor != null ? screenAnchor.position : cam.transform.position;
        Vector2 pos = rb.position;

        float screenRight = centerPos.x + width / 2f;
        float screenLeft = centerPos.x - width / 2f;
        float screenTop = centerPos.y + height / 2f;
        float screenBottom = centerPos.y - height / 2f;

        float rightTrigger = screenRight + screenPadding;
        float leftTrigger = screenLeft - screenPadding;
        float topTrigger = screenTop + screenPadding;
        float bottomTrigger = screenBottom - screenPadding;

        float halfWidth = col.bounds.extents.x;
        float halfHeight = col.bounds.extents.y;

        if (pos.x < screenLeft + halfWidth)
        {
            Vector2 dest = new Vector2(screenRight - halfWidth, pos.y);

            if (IsBlocked(dest, halfWidth, halfHeight))
            {
                rb.position = new Vector2(screenLeft + halfWidth, pos.y);
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            else if (pos.x < leftTrigger)
            {
                rb.position = dest;
            }
        }
        else if (pos.x > screenRight - halfWidth)
        {
            Vector2 dest = new Vector2(screenLeft + halfWidth, pos.y);

            if (IsBlocked(dest, halfWidth, halfHeight))
            {
                rb.position = new Vector2(screenRight - halfWidth, pos.y);
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            else if (pos.x > rightTrigger)
            {
                rb.position = dest;
            }
        }

        if (pos.y > screenTop - halfHeight)
        {
            Vector2 dest = new Vector2(pos.x, screenBottom + halfHeight);

            if (IsBlocked(dest, halfWidth, halfHeight))
            {
                rb.position = new Vector2(pos.x, screenTop - halfHeight);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
            else if (pos.y > topTrigger)
            {
                rb.position = dest;
            }
        }
        else if (pos.y < screenBottom + halfHeight)
        {
            Vector2 dest = new Vector2(pos.x, screenTop - halfHeight);

            if (IsBlocked(dest, halfWidth, halfHeight))
            {
                rb.position = new Vector2(pos.x, screenBottom + halfHeight);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
            else if (pos.y < bottomTrigger)
            {
                rb.position = dest;
            }
        }
    }

    bool IsBlocked(Vector2 targetPos, float halfWidth, float halfHeight)
    {
        float safeWidth = Mathf.Max(0.01f, (halfWidth * 2f) - 0.1f);
        float safeHeight = Mathf.Max(0.01f, (halfHeight * 2f) - 0.1f);
        Vector2 size = new Vector2(safeWidth, safeHeight);
        
        return Physics2D.OverlapBox(targetPos, size, 0f, wallLayer);
    }

    void OnDrawGizmos()
    {
        if (cam == null) cam = Camera.main;
        if (col == null) col = GetComponent<Collider2D>();
        if (cam == null || col == null) return;

        float h = cam.orthographicSize * 2f;
        float w = h * cam.aspect;
        Vector3 centerPos = screenAnchor != null ? screenAnchor.position : cam.transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(centerPos, new Vector3(w + (screenPadding * 2f), h + (screenPadding * 2f), 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(centerPos, new Vector3(w, h, 0));

        Gizmos.color = Color.red;
        float safeWidth = Mathf.Max(0.01f, (col.bounds.extents.x * 2f) - 0.1f);
        float safeHeight = Mathf.Max(0.01f, (col.bounds.extents.y * 2f) - 0.1f);
        Vector2 size = new Vector2(safeWidth, safeHeight);
        Gizmos.DrawWireCube(transform.position, size);
    }
}