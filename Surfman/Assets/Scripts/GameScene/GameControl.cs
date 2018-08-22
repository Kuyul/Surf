using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour {

    public static GameControl instance;

    public GameObject[] boardTypes;

    //Patterns to spawn - Easy/Normal/Hard
    public GameObject[] EasyPatterns;
    public GameObject[] NormalPatterns;
    public GameObject[] HardPatterns;
    public int NextLevelScore = 10000;
    public int PlayerSpeedIncremental = 2;

    //Parameters required for first patter spawn Logic
    public Transform SpawnPoint;
    public GameObject TutorialPattern;
    public GameObject StartPattern;

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
    public AudioSource starSound;

    public int scoreIncremental = 10;
    private int timeIncremental = 1;
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
    public float island;

    public bool isDead = false;

    private int currentScore = 0;
    //variable to decide when to show interstitial ad
    private int deathCount = 0;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < boardTypes.Length; i++)
        {
            if(PlayerPrefs.GetInt("board"+i) == 2)
            {
                boardTypes[i].SetActive(true);
            }

            if (PlayerPrefs.GetInt("board" + i) != 2)
            {
                boardTypes[i].SetActive(false);
            }
        }


    }

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.GetInt("ComTutorial") == 0)
        {
            TutorialPattern.SetActive(true);
        }
        else
        {
            StartPattern.SetActive(true);
        }

        //Display the last guy on the leaderboard
        UpdateNextHighscore();

        backgroundMusic.volume = PlayerPrefs.GetFloat("gamevolume",1f);
        onEdibleTakeSound.volume = PlayerPrefs.GetFloat("gamevolume",1f);
        balloonPopSound.volume = PlayerPrefs.GetFloat("gamevolume",1f);
        jumpSound.volume = PlayerPrefs.GetFloat("gamevolume",1f);
        starSound.volume = PlayerPrefs.GetFloat("gamevolume", 1f);

        //Increase score every millisecond
        InvokeRepeating("IncrementScorePerFrame", 0.0f, 0.0166f);
    }
	
	// Update is called once per frame
	void Update () {

        //Increment score per frame
        if (isDead) 
        {
            CancelInvoke();
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
        currentScore = currentScore + timeIncremental;
        //Update next highscore panel
        nextScoreInt = nextScoreInt - timeIncremental;
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

    public void SetTimeIncremental(int frame)
    {
        timeIncremental = frame;
    }

    public void Die()
    {
        //Functions to perform after death
        StartCoroutine(WaitDieAnimation());
        scoreTextInGame.text = "";
        timeIncremental = 0;
        isDead = true;
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

    //Wait 1 second before showing death panel
    IEnumerator WaitDieAnimation()
    {
        yield return new WaitForSeconds(1);
        Admob.Instance.ShowInterstitial();
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

    public GameObject[] FetchPatterns()
    {
        //return set of patterns for the pattern generator to use based on the current score
        if (currentScore < NextLevelScore)
        {
            return EasyPatterns;
        }
        else if (currentScore >= NextLevelScore && currentScore < 2*NextLevelScore)
        {
            PlayerControl.instance.PlayerSpeed = 9;
            return NormalPatterns;
        }
        else
        {
            PlayerControl.instance.PlayerSpeed = 11;
            return HardPatterns;
        }
    }
}
