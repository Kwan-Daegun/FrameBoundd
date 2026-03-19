using UnityEngine;
using Unity.Cinemachine;

public class CameraFreeze : MonoBehaviour
{
    public CinemachineCamera vcam;
    public Transform player;

    bool isFrozen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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