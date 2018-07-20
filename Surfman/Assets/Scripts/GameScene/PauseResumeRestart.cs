using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResumeRestart : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject muteButton;
    public GameObject unmuteButton;
    public AudioSource buttonFeedback;

    public void PauseGame()
    {
        isPaused = true;
        if (isPaused == true)
        {
            Time.timeScale = 0;
            GameControl.instance.SetTimeIncremental(0);
            GameControl.instance.backgroundMusic.Pause();
            GameControl.instance.pausePanel.SetActive(true);
            GameControl.instance.scoreTextInPause.text = "Your Highscore is " + PlayerPrefs.GetInt("highscore");
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (isPaused == false)
        {
            Time.timeScale = 1;
            GameControl.instance.SetTimeIncremental(1);
            GameControl.instance.pausePanel.SetActive(false);
            GameControl.instance.backgroundMusic.UnPause();
        }
    }

    public void RestartGame(int level)
    {
        SceneManager.LoadScene(level);
        isPaused = false;
        Time.timeScale = 1;
        GameControl.instance.pausePanel.SetActive(false);
    }

    public void LoadHome(int homeScene)
    {
        SceneManager.LoadScene(homeScene);
    }

    public void LoadGame(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void MuteSounds()
    {
        PlayerPrefs.SetFloat("gamevolume", 0f);

            muteButton.SetActive(false);
            unmuteButton.SetActive(true); 
    }

    public void UnMuteSounds()
    {
        PlayerPrefs.SetFloat("gamevolume", 1f);

            unmuteButton.SetActive(false);
            muteButton.SetActive(true); 
    }

    public void PlayFeedback()
    {
        buttonFeedback.Play();
    }


    //public void ResetHighscore()
    //{
    //      PlayerPrefs.DeleteKey("highscore");
    //}

}
