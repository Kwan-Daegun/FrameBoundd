using UnityEngine;
using UnityEngine.EventSystems;

public class TextEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale Settings")]
    public float horizontalScaleMultiplier = 1.2f;
    public float verticalScaleMultiplier = 1.2f;

    [Header("Floating Settings")]
    public float floatAmplitude = 10f;     
    public float floatSpeed = 2f;          

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.localPosition = new Vector3(
            originalPosition.x,
            originalPosition.y + offsetY,
            originalPosition.z
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(
            originalScale.x * horizontalScaleMultiplier,
            originalScale.y * verticalScaleMultiplier,
            originalScale.z
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}