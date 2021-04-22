using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables
    public Resolution[] resolutions;
    public AudioMixer masterSound;
    public GameObject loadingScreen;
    public TextMeshProUGUI progressText;
    public Image loadingBar;
    public Toggle fullscreenToggle;
    public Slider musicSlider;
    public Dropdown resoloutionDropdown;
    #endregion

    #region Audio Settings
    public void ChangeAudio(float volume)
    {
        masterSound.SetFloat("volume", volume);
    }
    public void ChangeSoundEffects(float soundEffects)
    {
        masterSound.SetFloat("SoundEffects", soundEffects);
    }
    public void MuteAudio(bool isMuted)
    {
        if (isMuted)
        {
            masterSound.SetFloat("isMuted", -80);
        }
        else
        {
            masterSound.SetFloat("isMuted", 0);
        }
    }
    #endregion

    #region Quality Settings
    public void Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion

    #region Video Settings
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void Start()
    {
        LoadPlayerPrefs();
        SetUpResoloutions();

    }
    public void SetUpResoloutions()
    {
        resolutions = Screen.resolutions;

        resoloutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResoloutionIndex = 0;
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height + "@" + resolution.refreshRate;
            options.Add(option);
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height && resolution.refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResoloutionIndex = options.Count - 1;
            }


        }
        resoloutionDropdown.AddOptions(options);
        resoloutionDropdown.value = currentResoloutionIndex;
        resoloutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    #endregion

    #region Loading
    public void SwitchScene(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
    IEnumerator LoadAsyncronysly(int levelID)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelID);
        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            loadingBar.fillAmount = progress;
            progressText.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
    }
    public void LoadAsync(int levelID)
    {
        StartCoroutine(LoadAsyncronysly(levelID));
    }
    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        Debug.Log("Game Exited");
    }
    #endregion
    #region Player Prefs
    public void SavePlayerPrefs()
    {
        float volume;
        if (masterSound.GetFloat("volume", out volume))
        {
            PlayerPrefs.SetFloat("volume", volume);
        }

        PlayerPrefs.SetInt("fullscreen", fullscreenToggle.isOn ? 0 : 1);
    }
    public void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float sound = PlayerPrefs.GetFloat("volume");
            masterSound.SetFloat("volume", sound);
            musicSlider.value = sound;
        }
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            fullscreenToggle.isOn = PlayerPrefs.GetInt("fullscreen") == 0 ? false : true;
            SetFullscreen(fullscreenToggle.isOn);
        }
    }
    private void OnDisable()
    {
        SavePlayerPrefs();
    }
    #endregion
}
