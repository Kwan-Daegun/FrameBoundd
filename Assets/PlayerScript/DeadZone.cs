using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Movement movement = other.GetComponent<Movement>();

        if (movement != null)
        {
            movement.Respawn();
        }
        else
        {
            Debug.LogWarning("DeadZone hit the player, but Movement was not found.");
        }
    }
}
