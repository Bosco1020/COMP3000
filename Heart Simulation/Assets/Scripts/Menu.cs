using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public static bool IsPaused = false;

    public Slider speedSlider;

    public TextMeshProUGUI speedvalue;

    public UnityEvent resumeGame, pauseGame;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        pauseGame.Invoke();
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        resumeGame.Invoke();
        IsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (!IsPaused)
        {
            Time.timeScale = speedSlider.value;
            speedvalue.SetText("x" + speedSlider.value.ToString("0.00"));
        }
    }
}
