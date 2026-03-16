using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image leftBar;
    public Image rightBar;
    public float fillSpeed = 0.5f;
    public float drainSpeed = 0.5f;

    private bool filling = false;
    private bool hasChased = false;   // 🔥 aby sa nevolalo 100x

    public NPCController npcController;

    void Start()
    {
        leftBar.fillAmount = 0f;
        rightBar.fillAmount = 0f;
    }

    void Update()
    {
        if (filling)
        {
            leftBar.fillAmount += fillSpeed * Time.deltaTime;
            rightBar.fillAmount += fillSpeed * Time.deltaTime;
        }
        else
        {
            leftBar.fillAmount -= drainSpeed * Time.deltaTime;
            rightBar.fillAmount -= drainSpeed * Time.deltaTime;
        }

        leftBar.fillAmount = Mathf.Clamp01(leftBar.fillAmount);
        rightBar.fillAmount = Mathf.Clamp01(rightBar.fillAmount);

        // 🔥 keď sa naplní prvýkrát
        if (leftBar.fillAmount >= 1f && !hasChased)
        {
            npcController.StartChase();
            hasChased = true;
        }

        // 🔥 keď sa úplne vyprázdni
        if (leftBar.fillAmount <= 0f && hasChased)
        {
            npcController.StopChase();
            hasChased = false;
        }
    }

    public void StartFilling()
    {
        filling = true;
    }

    public void StopFilling()
    {
        filling = false;
    }
}