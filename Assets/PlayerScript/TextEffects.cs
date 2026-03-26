using UnityEngine;
using UnityEngine.EventSystems;

public class TextEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float horizontalScaleMultiplier = 1.2f;
    public float verticalScaleMultiplier = 1.2f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(originalScale.x * horizontalScaleMultiplier, originalScale.y * verticalScaleMultiplier, originalScale.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}