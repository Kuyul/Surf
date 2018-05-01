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

    public Text scoreText;
    public Text scoreTextAtEnd;

    public int scrollSpeed;

    public bool isDead = false;

    private int highscore=0;

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
        highscore = highscore + 1;
        scoreText.text = "Score: " + highscore;
    }

    public void Die()
    {
        scoreText.text = "";
        isDead = true;
        backgroundMusic.Pause();
        pauseButtonPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        scoreTextAtEnd.text = "Your Score is " + highscore;
    }


}
