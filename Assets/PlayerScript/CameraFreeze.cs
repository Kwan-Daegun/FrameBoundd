using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraFreeze : MonoBehaviour
{
    public CinemachineCamera vcam;
    public Transform player;

    bool isFrozen;

    
    public void OnFreeze(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleFreeze();
        }
    }

    public void ToggleFreeze()
    {
        if (isFrozen)
        {
            vcam.Follow = player;
            isFrozen = false;
        }
        else
        {
            vcam.Follow = null;
            isFrozen = true;
        }
    }
}