using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float blinkSpeed = 2f;

    private TextMeshProUGUI text;
    private bool isBlinking = false;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();


        SetAlpha(1f);


        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isBlinking) return;

        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        SetAlpha(alpha);
    }

    public void Show()
    {
        if (gameObject.activeSelf) return;

        gameObject.SetActive(true);
        isBlinking = true;
    }

    public void Hide()
    {
        if (!gameObject.activeSelf) return;

        isBlinking = false;


        SetAlpha(1f);

        gameObject.SetActive(false);
    }

    private void SetAlpha(float a)
    {
        Color c = text.color;
        c.a = a;
        text.color = c;
    }
}