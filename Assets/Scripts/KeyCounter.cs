using TMPro;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    public TextMeshProUGUI keyText;

    public int currentKeys = 0;
    public int maxKeys = 6;

    void Start()
    {
        UpdateUI();
    }

    public void AddKey()
    {
        currentKeys++;
        UpdateUI();
    }

    void UpdateUI()
    {
        keyText.text = currentKeys + " / " + maxKeys;
    }
}