using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Sources")]
    public AudioSource backgroundSource;
    public AudioSource chaseSource;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip chaseMusic;

    [Header("Volume")]
    public float backgroundVolume = 0.2f;
    public float chaseVolume = 0.1f;

    [Header("Fade")]
    public float fadeDuration = 1.5f;

    private int chaseCount = 0;
    private Coroutine currentFade;
    public float masterVolume = 1f;

    public bool isChaseActive = false;

    void Awake()
{
    if (instance != null && instance != this)
    {
        Destroy(gameObject);
        return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);

    
    AudioSource[] sources = GetComponents<AudioSource>();

    if (sources.Length >= 2)
    {
        backgroundSource = sources[0];
        chaseSource = sources[1];
    }
    else
    {
        Debug.LogError("Potrebujem 2 AudioSource komponenty!");
    }
}

    void Start()
    {
        backgroundSource.clip = backgroundMusic;
        backgroundSource.volume = backgroundVolume;
        backgroundSource.loop = true;
        backgroundSource.Play();

        chaseSource.clip = chaseMusic;
        chaseSource.volume = 0f;
        chaseSource.loop = true;
        chaseSource.Play();
    }

    public void StartChase()
    {
        chaseCount++;

        if (chaseCount == 1)
        {
            isChaseActive = true;
            StartCrossfade(backgroundSource, chaseSource);
        }
    }

    public void StopChase()
    {
        chaseCount--;

        if (chaseCount <= 0)
        {
            chaseCount = 0;
            isChaseActive = false;
            StartCrossfade(chaseSource, backgroundSource);
        }
    }

    void StartCrossfade(AudioSource from, AudioSource to)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(Crossfade(from, to));
    }

    IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        float time = 0;

        float startFromVolume = from.volume;
        float baseVolume = (to == backgroundSource) ? backgroundVolume : chaseVolume;
            float targetToVolume = baseVolume * masterVolume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float t = time / fadeDuration;

            from.volume = Mathf.Lerp(startFromVolume, 0f, t);
            to.volume = Mathf.Lerp(0f, targetToVolume, t);

            yield return null;
        }

        from.volume = 0f;
        to.volume = targetToVolume;
    }

    public void StopAllMusicSmooth()
{
    if (currentFade != null)
        StopCoroutine(currentFade);

    currentFade = StartCoroutine(FadeOutAll());
}

IEnumerator FadeOutAll()
{
    float time = 0;

    float startBg = backgroundSource.volume;
    float startChase = chaseSource.volume;

    while (time < fadeDuration)
    {
        time += Time.unscaledDeltaTime; // 👈 dôležité!

        float t = time / fadeDuration;

        backgroundSource.volume = Mathf.Lerp(startBg, 0f, t);
        chaseSource.volume = Mathf.Lerp(startChase, 0f, t);

        yield return null;
    }

    backgroundSource.Stop();
    chaseSource.Stop();
}

public void PlayBackgroundMusic()
{
    backgroundSource.volume = backgroundVolume * masterVolume;
    chaseSource.volume = 0f;

    if (!backgroundSource.isPlaying)
        backgroundSource.Play();

    if (!chaseSource.isPlaying)
        chaseSource.Play();

    isChaseActive = false;
    chaseCount = 0;
}
}