using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl instance;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject pauseButtonPanel;  
    
    public AudioSource backgroundMusic;
    public AudioSource onEdibleTakeSound;
    public AudioSource balloonPopSound;
    public AudioSource jumpSound;

    public int scoreIncremental = 10;
    private int frameIncremental = 1;
    public Text scoreTextInGame;
    public Text scoreTextAtEnd;
    public Text highScoreTextAtEnd;

    public int scrollSpeed;

    public bool isDead = false;

    private int currentScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementScore()
    {
        currentScore = currentScore + scoreIncremental;
        UpdateScoreboard();
    }

    public void IncrementScorePerFrame()
    {
        currentScore = currentScore + frameIncremental;
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        scoreTextInGame.text = "Score: " + currentScore;

        if (currentScore > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
    }

    public void Die()
    {
        scoreTextInGame.text = "";
        frameIncremental = 0;
        isDead = true;
        backgroundMusic.Pause();
        pauseButtonPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreTextAtEnd.text = "Your Score is " + currentScore;
        highScoreTextAtEnd.text = "Your HighScore is " + PlayerPrefs.GetInt("highscore");
    }


}
