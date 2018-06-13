using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour {

    public static GameControl instance;

    //Next Highscore components
    public GameObject nextProfilePic;
    public Text nextScoreText;
    private int nextScoreInt;
    private int LeaderPos = 0;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject pausedButtonPanel;  
    
    public AudioSource backgroundMusic;
    public AudioSource onEdibleTakeSound;
    public AudioSource balloonPopSound;
    public AudioSource jumpSound;

    public int scoreIncremental = 10;
    private int frameIncremental = 1;
    public Text scoreTextInGame;
    public Text scoreTextAtEnd;
    public Text highScoreTextAtEnd;
    public Text scoreTextInPause;

    public float scrollSpeed;
    public float waveOneSpeed;
    public float waveTwoSpeed;
    public float waveThreeSpeed;
    public float waveFourSpeed;
    public float waveFiveSpeed;
    public float boat;
    public float cloud1;
    public float cloud2;
    public float cloud3;

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
        UpdateNextHighscore();
    }
	
	// Update is called once per frame
	void Update () {

        //Increment score per frame
        if (!isDead) 
        {
            IncrementScorePerFrame();
        }
    }

    public void IncrementScore()
    {
        if (!isDead)
        {
            currentScore = currentScore + scoreIncremental;
            //Update next highscore panel
            nextScoreInt = nextScoreInt - scoreIncremental;
            UpdateScoreboard();
        }
    }

    public void IncrementScorePerFrame()
    {
        currentScore = currentScore + frameIncremental;
        //Update next highscore panel
        nextScoreInt = nextScoreInt - frameIncremental;
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        scoreTextInGame.text = "Score: " + currentScore;
        nextScoreText.text = "" + nextScoreInt;
        if (nextScoreInt <= 0)
        {
            UpdateNextHighscore();
        }
    }

    public void SetFrameIncremental(int frame)
    {
        frameIncremental = frame;
    }

    public void Die()
    {
        scoreTextInGame.text = "";
        frameIncremental = 0;
        isDead = true;
        backgroundMusic.Pause();
        pausedButtonPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreTextAtEnd.text = "Your Score is " + currentScore;
        highScoreTextAtEnd.text = "Your HighScore is " + PlayerPrefs.GetInt("highscore");
        if (currentScore > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
        LeaderboardControl.Instance.UpdateHighScore();
    }


    //Update Next highscore for the player to chase!
    public void UpdateNextHighscore()
    {
        LeaderboardControl.Instance.SortLeaders(false);
        int count = LeaderboardControl.Instance.Leaders.Count;
        if (LeaderPos < count)
        {
            LeaderboardEntry e = LeaderboardControl.Instance.Leaders[LeaderPos];
            Image image = nextProfilePic.GetComponent<Image>();
            image.sprite = e.getProfileSprite();
            nextScoreInt = Convert.ToInt32(e.getScore()) - currentScore;
            nextScoreText.text = "" + nextScoreInt;
            LeaderPos++;
        }
    }
}
