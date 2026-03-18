using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public float sfxVolume = 1f;

    void Awake()
    {
        instance = this;
    }
}