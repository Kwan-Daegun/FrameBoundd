using UnityEngine;
using Unity.Cinemachine;

public class Door : MonoBehaviour
{
    [Header("UI")]
    public BlinkText warningText;

    [Header("Key Requirement")]
    public int requiredKeys = 3;

    [Header("Movement")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    [Header("Shake Settings")]
    public float shakeInterval = 0.1f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isOpening = false;

    private CinemachineImpulseSource impulseSource;
    private float shakeTimer;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void OnEnable()
    {
        KeyUI.onKeyChanged += CheckKeys;
    }

    void OnDisable()
    {
        KeyUI.onKeyChanged -= CheckKeys;
    }

    void CheckKeys(int count)
    {
        if (count >= requiredKeys && !isOpening)
        {
            isOpening = true;

            
            if (warningText != null)
            {
                warningText.Show();
            }

            
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }
        }
    }

    void Update()
    {
        if (!isOpening) return;

        
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        
        if (impulseSource != null && transform.position != targetPos)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                impulseSource.GenerateImpulse();
                shakeTimer = shakeInterval;
            }
        }

        
        if (transform.position == targetPos)
        {
            isOpening = false;

            if (warningText != null)
            {
                warningText.Hide();
            }
        }
    }
}