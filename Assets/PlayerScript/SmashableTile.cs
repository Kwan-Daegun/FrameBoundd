using UnityEngine;
using Unity.Cinemachine;

public class SmashableTile : MonoBehaviour
{
    public GameObject breakEffect;
    public CinemachineImpulseSource impulseSource;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Movement playerMovement = collision.gameObject.GetComponent<Movement>();

        if (playerMovement != null)
        {
            float impactSpeed = collision.relativeVelocity.magnitude;
            float requiredSpeed = Mathf.Abs(playerMovement.maxFallSpeed) * 0.95f;

            if (impactSpeed >= requiredSpeed)
            {
                if (breakEffect != null)
                {
                    Vector2 spawnPos = collision.GetContact(0).point;
                    GameObject effect = Instantiate(breakEffect, spawnPos, Quaternion.identity);
                    effect.transform.localScale = Vector3.one;
                    Destroy(effect, 2f);
                }

                if (impulseSource != null)
                {
                    impulseSource.GenerateImpulse();
                }

                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, playerMovement.maxFallSpeed);
                }

                Destroy(gameObject);
            }
        }
    }
}