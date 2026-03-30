using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class CameraFreeze : MonoBehaviour
{
    public CinemachineCamera vcam;
    public Transform player;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private float popupDuration = 1.2f;

    bool isFrozen;
    Coroutine popupRoutine;

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
            ShowPopup("UNFREEZE");
        }
        else
        {
            vcam.Follow = null;
            isFrozen = true;
            ShowPopup("FREEZE");
        }
    }

    void ShowPopup(string message)
    {
        if (popupText == null) return;

        if (popupRoutine != null)
            StopCoroutine(popupRoutine);

        popupRoutine = StartCoroutine(PopupRoutine(message));
    }

    IEnumerator PopupRoutine(string message)
    {
        popupText.text = message;
        popupText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(popupDuration);

        popupText.gameObject.SetActive(false);
    }
}