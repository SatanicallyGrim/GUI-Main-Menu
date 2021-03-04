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
    public Dropdown resolution;
    public AudioMixer masterSound;
    public GameObject loadingScreen;
    public TextMeshProUGUI progressText;
    public Image loadingBar;
    #endregion
    #region Audio Settings
    public void ChangeAudio(float volume)
    {
        masterSound.SetFloat("volume", volume);
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
        
        resolutions = Screen.resolutions;
        resolution.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    #endregion

    
    public void SwitchScene(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
    IEnumerator LoadAsyncronysly(int levelID)
    {
        loadingScreen.SetActive(true);
        //yield return new WaitForSeconds(1);
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
}
