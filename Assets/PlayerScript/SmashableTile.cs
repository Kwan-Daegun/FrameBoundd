using UnityEngine;
using Unity.Cinemachine;
public class SmashableTile : MonoBehaviour
{
  public float breakVelocity = -10f;
    public GameObject breakEffect;
    public CinemachineImpulseSource impulseSource;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        float impactSpeed = collision.relativeVelocity.y;

        if (impactSpeed < breakVelocity)
        {
            if (breakEffect != null)
            {
                GameObject effect = Instantiate(
                    breakEffect,
                    transform.position,
                    Quaternion.identity
                );

                Destroy(effect, 2f);
            }

            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse();
            }

            Destroy(gameObject);
        }
    }
}
