using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gamepaused;
    public GameObject pausePanel, optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                pausePanel.SetActive(true);
            }
            else
            {
                gamepaused = !gamepaused;
                if (gamepaused)
                {
                    Pause();
                }
                else
                {
                    ResumeGame();
                }
            }

        }
    }
    public void Pause()
    {
        gamepaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        gamepaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
