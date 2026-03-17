using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "GameScene";

    public GameObject mainPanel;
    public GameObject settingsPanel;

    public Slider musicSlider;
    public Slider masterSlider;


    void Start()
    {
        // load uložených hodnôt
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 1f);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
        AudioListener.volume = value; // globálny zvuk
    }
}