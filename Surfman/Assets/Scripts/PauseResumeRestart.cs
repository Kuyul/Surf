using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResumeRestart : MonoBehaviour {

    public bool isPaused = false;
    public GameObject pausePanel;

    public void PauseGame()
    {
        isPaused = true;
        if (isPaused == true)
        {
            Time.timeScale = 0;
            GameControl.instance.backgroundMusic.Pause();
            pausePanel.SetActive(true);

        }

    }

    public void ResumeGame()
    {
        isPaused = false;
        if(isPaused==false)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            GameControl.instance.backgroundMusic.UnPause();
        }
    }

    public void RestartGame(int level)
    {
        SceneManager.LoadScene(level);
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }


}
