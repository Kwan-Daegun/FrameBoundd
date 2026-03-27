using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Hover Settings")]
    public float hoverHeight = 0.25f;
    public float hoverSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (KeyUI.instance != null)
            {
                KeyUI.instance.AddKey();
            }
            Destroy(gameObject);
        }
    }
}