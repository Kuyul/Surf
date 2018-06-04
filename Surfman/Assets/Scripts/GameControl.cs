using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    //Next Highscore components
    public GameObject nextProfilePic;
    public GameObject nextScore;

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
    public Text scoreTextInPause;

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
            UpdateScoreboard();
        }
    }

    public void IncrementScorePerFrame()
    {
        currentScore = currentScore + frameIncremental;
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        scoreTextInGame.text = "Score: " + currentScore;
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
        pauseButtonPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreTextAtEnd.text = "Your Score is " + currentScore;
        highScoreTextAtEnd.text = "Your HighScore is " + PlayerPrefs.GetInt("highscore");
        if (currentScore > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
        LeaderboardControl.Instance.UpdateHighScore();
    }

    public void UpdateNextHighscore()
    {
        LeaderboardEntry e = LeaderboardControl.Instance.Leaders[1];
        Image image = nextProfilePic.GetComponent<Image>();
        image.sprite = e.getProfileSprite();
        Text text = nextScore.GetComponent<Text>();
        text.text = e.getScore();
    }
}
