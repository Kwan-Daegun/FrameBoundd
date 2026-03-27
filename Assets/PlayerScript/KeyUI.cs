using UnityEngine;
using TMPro;

public class KeyUI : MonoBehaviour
{
    public static KeyUI instance;

    public TextMeshProUGUI keyText;
    private int keyCount = 0;

    
    public delegate void OnKeyChanged(int count);
    public static event OnKeyChanged onKeyChanged;

    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    public void AddKey()
    {
        keyCount++;
        UpdateUI();

        
        onKeyChanged?.Invoke(keyCount);
    }

    public int GetKeyCount()
    {
        return keyCount;
    }

    void UpdateUI()
    {
        keyText.text = "X " + keyCount;
    }
}