using TMPro;
using UnityEngine;
using System.Collections;

public class KeyCounter : MonoBehaviour
{
    public TextMeshProUGUI keyText;

    public int currentKeys = 0;
    public int maxKeys = 6;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = keyText.rectTransform.localPosition;
        UpdateUI();
    }

    public void AddKey()
    {
        currentKeys++;
        UpdateUI();
    }

    public void ShakeText()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float duration = 0.3f;
        float strength = 5f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * strength;
            float offsetY = Random.Range(-1f, 1f) * strength;

            keyText.rectTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        keyText.rectTransform.localPosition = originalPos;
    }

    void UpdateUI()
    {
        keyText.text = currentKeys + " / " + maxKeys;
    }
}